using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TuanVinhShop.Model.Models;
using TuanVinhShop.Service;
using TuanVinhShop.Web.Inflastructure.Core;
using TuanVinhShop.Web.Inflastructure.Extensions;
using TuanVinhShop.Web.Models;

namespace TuanVinhShop.Web.Api
{
    [RoutePrefix("api/productCategory")]
    [AllowAnonymous]
    public class ProductCategoryController : ApiControllerBase
    {
        
        IProductCategoryService _productCategoryService;
        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getall")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var resultGetAll = _productCategoryService.GetAll();
                var mapperObject = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(resultGetAll);
                response = request.CreateResponse(HttpStatusCode.OK, mapperObject);

                return response;
            });
        }

        [Route("getPagination")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetPagination(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                int totalRow = 0;

                var model = _productCategoryService.GetPagin(keyword, page, pageSize, out totalRow);

                var mapperObject = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);

                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = mapperObject,
                    PageIndex = page,
                    PageSize = pageSize,
                    TotalRows = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productVM)
        {

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    ProductCategory product = new ProductCategory();
                    product.UpdateProductCategory(productVM);
                    var result = _productCategoryService.Add(product);
                    var mapperResult = Mapper.Map<ProductCategory, ProductCategoryViewModel>(result);
                    _productCategoryService.SaveChange();
                    response = request.CreateResponse(HttpStatusCode.Created, mapperResult);
                }
                return response;
            });


        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var updateProduct = _productCategoryService.GetById(productVM.ID);
                    updateProduct.UpdateProductCategory(productVM);

                    _productCategoryService.Update(updateProduct);
                    _productCategoryService.SaveChange();
                    var mapper = Mapper.Map<ProductCategory, ProductCategoryViewModel>(updateProduct);
                    response = request.CreateResponse(HttpStatusCode.Created, mapper);
                }
                return response;
            });
        }

        [Route("detele")]
        public HttpResponseMessage Detele(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var result = _productCategoryService.Delete(id);
                _productCategoryService.SaveChange();
                response = request.CreateResponse(HttpStatusCode.OK, result);

                return response;
            });
        }
    }
}