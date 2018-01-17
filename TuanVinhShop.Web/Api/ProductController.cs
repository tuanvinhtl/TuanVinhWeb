using AutoMapper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
        public HttpResponseMessage GetPagination(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                int totalRow = 0;

                var model = _productService.GetPagin(keyword, page, pageSize, out totalRow);

                var mapperObject = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);

                var paginationSet = new PaginationSet<ProductViewModel>()
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
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Detele(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var result = _productService.Delete(int.Parse(id));
                _productService.SaveChange();
                response = request.CreateResponse(HttpStatusCode.OK, result);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var resultGetAll = _productService.GetById(id);
                var mapperObject = Mapper.Map<Product, ProductViewModel>(resultGetAll);
                response = request.CreateResponse(HttpStatusCode.OK, mapperObject);
                return response;
            });
        }

        [Route("export/{id}")]
        [HttpGet]
        public HttpResponseMessage ExportProduct(HttpRequestMessage request, int id)
        {
            var folderReport = "/Reports";
            string document = OutExcelAll();
            return request.CreateErrorResponse(HttpStatusCode.OK, folderReport + "/" + document);
        }


        private string OutExcel(int productId)
        {
            var folderReport = "/Reports";
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            // template File
            string templateDocument = HttpContext.Current.Server.MapPath("~/Reports/TemplateForReport/ProductReport.xlsx");
            string documentName = string.Format("Product-{0}-{1}.xlsx", productId, DateTime.Now.ToString("ddmmyyyy"));
            string fullPath = Path.Combine(filePath, documentName);
            //result Output
            MemoryStream output = new MemoryStream();

            //read template
            FileStream templateDocumentStream = File.OpenRead(templateDocument);
            ExcelPackage package = new ExcelPackage(templateDocumentStream);
            ExcelWorksheet sheet = package.Workbook.Worksheets["ProductReportId"];
            var product = _productService.GetById(productId);
            sheet.Cells[6, 1].Value = productId;
            sheet.Cells[6, 2].Value = product.Name;
            sheet.Cells[6, 3].Value = product.Alias;
            sheet.Cells[6, 4].Value = product.Price;
            sheet.Cells[6, 5].Value = product.CategoryID;
            sheet.Cells[6, 6].Value = product.Status;
            package.SaveAs(new FileInfo(fullPath));
            return documentName;
        }

        private string OutExcelAll()
        {
            var folderReport = "/Reports";
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            // template File
            string templateDocument = HttpContext.Current.Server.MapPath("~/Reports/TemplateForReport/ProductReport.xlsx");
            string documentName = string.Format("Product-{0}.xlsx", DateTime.Now.ToString("ddmmyyyyss"));
            string fullPath = Path.Combine(filePath, documentName);
            //result Output
            MemoryStream output = new MemoryStream();

            //read template
            FileStream templateDocumentStream = File.OpenRead(templateDocument);
            ExcelPackage package = new ExcelPackage(templateDocumentStream);
            ExcelWorksheet sheet = package.Workbook.Worksheets["ProductReportId"];
            var product = _productService.GetAll();
            int i = 0;
            foreach (var item in product)
            {
                i += 1;
                sheet.Cells[6 + i, 1].Value = item.ID;
                sheet.Cells[6 + i, 2].Value = item.Name;
                sheet.Cells[6 + i, 3].Value = item.Alias;
                sheet.Cells[6 + i, 4].Value = item.Price;
                sheet.Cells[6 + i, 5].Value = item.CategoryID;
                sheet.Cells[6 + i, 6].Value = item.Status;
            }
            package.SaveAs(new FileInfo(fullPath));

            return documentName;
        }
    }
}