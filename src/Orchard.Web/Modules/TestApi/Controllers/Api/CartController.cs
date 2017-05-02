using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TestApi.Services;

namespace TestApi.Controllers.Api
{
    public class CartController : ApiController {
        private readonly IShoppingCart _shoppingCart;

        public CartController(IShoppingCart shoppingCart) {
            _shoppingCart = shoppingCart;
        }

        public IHttpActionResult GetBook(int id)
        {
            return Ok();
        }

        public IHttpActionResult GetBooks()
        {
            return Ok();
        }

        [HttpPost]
        public HttpResponseMessage Add(int id) {
            _shoppingCart.Add(id);
            //var newUrl = Url.Link("ShoppingCart", new {controller = "ShoppingCart", action = "Index", area = "TestApi"});
            return Request.CreateResponse(HttpStatusCode.OK,
                new { Success = true});
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id) {
            return Ok();
        }
    }
}