using System.Web.Http;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;

namespace UsingRestApi.Controllers
{
    public class ProductController : ApiController {
        private readonly IContentManager _contentManager;
        public IOrchardServices Services { get; private set; }
        public Localizer T { get; set; }

        public ProductController(IContentManager contentManager) {
            _contentManager = contentManager;
        }

        public string Get() {
            return "test";
        }
    }
}