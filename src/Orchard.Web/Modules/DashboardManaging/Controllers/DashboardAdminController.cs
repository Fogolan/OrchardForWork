using System.Web.Mvc;
using DashboardManaging.Models;
using DashboardManaging.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Themes;

namespace DashboardManaging.Controllers
{
    public class DashboardAdminController : Controller
    {
        private readonly IWorkContextAccessor _workContextAccessor;

        public DashboardAdminController(IWorkContextAccessor workContextAccessor) {
            _workContextAccessor = workContextAccessor;
        }

        [Themed]
        public ActionResult Index()
        {
            return View();
        }

        [Themed]
        public ActionResult SecondItemIndex() {
            return View();
        }

        [Themed]
        public ActionResult GmailApi() {
            var workContext = _workContextAccessor.GetContext();
            var settings = workContext.CurrentSite.As<GmailApiSettingsPart>();
            var model = new GmailApiViewModel {
                ClientId = settings.ClientId,
                Scopes = settings.Scopes,
                DiscoveryDocs = settings.DiscoveryDocs
            };
            return View(model);
        }
    }
}