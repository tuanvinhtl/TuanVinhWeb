using System.Net;
using System.Net.Http;
using System.Web.Http;
using TuanVinhShop.Model.Models;
using TuanVinhShop.Service;
using TuanVinhShop.Web.Inflastructure.Core;

namespace TuanVinhShop.Web.Api
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiControllerBase
    {
        IProductService _productService;
        public ProductController(IErrorService errorService, IProductService productService) : base(errorService)
        {
            this._productService = productService;
        }

        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var result = _productService.GetAll();
                response = request.CreateResponse(HttpStatusCode.Created, result);

                return response;
            });
        }

        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, Product product)
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
                    var result = _productService.Add(product);
                    _productService.SaveChange();
                    response = request.CreateResponse(HttpStatusCode.Created, result);
                }
                return response;
            });


        }

        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, Product product)
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
                    _productService.Update(product);
                    _productService.SaveChange();
                    response = request.CreateResponse(HttpStatusCode.Created);
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
                response = request.CreateResponse(HttpStatusCode.Created, result);

                return response;
            });
        }
    }
}
