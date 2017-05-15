using System.Web.Mvc;
using Orchard;
using Orchard.Mvc;
using Orchard.Themes;
using TestApi.Services;

namespace TestApi.Controllers
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
            var shape = _services.New.ShoppingCart();
            return new ShapeResult(this, shape);
        }

        [Themed]
        public ActionResult Register() {
            var shape = _services.New.RegisterUser();
            return new ShapeResult(this, shape);
        }
        [Themed]
        public ActionResult Login()
        {
            var shape = _services.New.LoginUser();
            return new ShapeResult(this, shape);
        }
    }
}