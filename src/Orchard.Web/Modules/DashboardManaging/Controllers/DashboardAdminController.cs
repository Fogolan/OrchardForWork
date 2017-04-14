using System.Web.Mvc;
using Orchard.Themes;

namespace DashboardManaging.Controllers
{
    public class DashboardAdminController : Controller
    {
        [Themed]
        public ActionResult Index()
        {
            return View();
        }
    }
}