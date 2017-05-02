using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
namespace UsingRestApi.Attributes
{
    public class ApiKeyAuthorizationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext) {
            var query = actionContext.Request.RequestUri.ParseQueryString();
            var apiKey = query["apiKey"];
            var workContext = actionContext.ControllerContext.GetWorkContext();
            var settings = workContext.CurrentSite.As<WebServiceSettingsPart>();
        }
    }
}