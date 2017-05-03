using System.Linq;
using System.Web.Mvc;
using Orchard;
using Orchard.Mvc;
using Orchard.Themes;
using TestApi.Models;
using TestApi.Services;

namespace TestApi.Controllers.Api
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCart _shoppingCart;
        private readonly IOrchardServices _services;
        public ShoppingCartController(
            IShoppingCart shoppingCart,
            IOrchardServices services) {
            _shoppingCart = shoppingCart;
            _services = services;
        }

        [Themed]
        public ActionResult Index() {
            var shape = _services.New.ShoppingCart(
            );
            return new ShapeResult(this, shape);
        }
    }
}