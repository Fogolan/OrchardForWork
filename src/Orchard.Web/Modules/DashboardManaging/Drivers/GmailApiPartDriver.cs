using DashboardManaging.Models;
using DashboardManaging.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Orchard.UI.Admin;

namespace DashboardManaging.Drivers
{
    public class GmailApiPartDriver : ContentPartDriver<GmailApiPart> {
        private readonly IWorkContextAccessor _workContextAccessor;

        public GmailApiPartDriver(IWorkContextAccessor workContextAccessor) {
            _workContextAccessor = workContextAccessor;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }
        protected override DriverResult Editor(GmailApiPart part, dynamic shapeHelper) {
            var workContext = _workContextAccessor.GetContext();

            if (AdminFilter.IsApplied(workContext.HttpContext.Request.RequestContext)){
                return null;
            }

            return ContentShape("Parts_GmailApi_Fields", () => {
                var settings = workContext.CurrentSite.As<GmailApiSettingsPart>();
                if (workContext.CurrentUser != null)
                {
                    return null;
                }
                var viewModel = new GmailApiViewModel {
                    ClientId = settings.ClientId,
                    Scopes = settings.Scopes
                };
                return shapeHelper.EditorTemplate(TemplateName: "Parts.GmailApi.Fields", Module: viewModel, Prefix: Prefix);
            });

        }

        protected override DriverResult Editor(GmailApiPart part, IUpdateModel updater, dynamic shapeHelper) {
            var workContext = _workContextAccessor.GetContext();
            var settings = workContext.CurrentSite.As<GmailApiSettingsPart>();

            if (AdminFilter.IsApplied(workContext.HttpContext.Request.RequestContext)) {
                return null;
            }

            var context = workContext.HttpContext;

            return Editor(part, shapeHelper);
        }
    }
}