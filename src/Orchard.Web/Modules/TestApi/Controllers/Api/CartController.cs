using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Script.Serialization;
using TestApi.Models;
using TestApi.Services;
using TestApi.ViewModels;

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

        public IHttpActionResult GetBooks() {
            var books = _shoppingCart.GetBooks().Select(x => new {Name = x.Name, Id = x.Id}).ToArray();
            return Ok(books) ;
        }

        [HttpPost]
        public HttpResponseMessage Add(int id) {
            _shoppingCart.Add(id);
            //var newUrl = Url.Link("ShoppingCart", new {controller = "ShoppingCart", action = "Index", area = "TestApi"});
            return Request.CreateResponse(HttpStatusCode.OK,
                new { Success = true});
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id) {
            _shoppingCart.Remove(id);
            return Request.CreateResponse(HttpStatusCode.OK,
                new { Success = true });
        }
    }
}