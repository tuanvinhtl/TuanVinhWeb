using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
    [RoutePrefix("api/product")]
    [AllowAnonymous]
    public class ProductController : ApiControllerBase
    {
        IProductService _productService;
        public ProductController(IErrorService errorService, IProductService productService) : base(errorService)
        {
            this._productService = productService;
        }

        [Route("getall")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var resultGetAll = _productService.GetAll();
                var mapperObject = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(resultGetAll);
                response = request.CreateResponse(HttpStatusCode.OK, mapperObject);

                return response;
            });
        }

        [Route("getPagination")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetPagination(HttpRequestMessage request, string keyword, int page, int pageSize=20)
        {
            return CreateHttpResponse(request, () =>
            {
                
                HttpResponseMessage response = null;
                int totalRow = 0;

                var model = _productService.GetPagin(keyword,page,pageSize, out totalRow);
 
                var mapperObject = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);

                var paginationSet = new PaginationSet<ProductViewModel>()
                {
                    Items = mapperObject,
                    PageIndex = page,
                    PageSize=pageSize,
                    TotalRows = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel productVM)
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
                    Product product = new Product();
                    product.UpdateProduct(productVM);
                    var result = _productService.Add(product);
                    var mapperResult = Mapper.Map<Product, ProductViewModel>(result);
                    _productService.SaveChange();
                    response = request.CreateResponse(HttpStatusCode.Created, mapperResult);
                }
                return response;
            });


        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel productVM)
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
                    var updateProduct = _productService.GetById(productVM.ID);
                    updateProduct.UpdateProduct(productVM);

                    _productService.Update(updateProduct);
                    _productService.SaveChange();
                    var mapper = Mapper.Map<Product, ProductViewModel>(updateProduct);
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

                var result = _productService.Delete(id);
                _productService.SaveChange();
                response = request.CreateResponse(HttpStatusCode.OK, result);

                return response;
            });
        }
    }
}
