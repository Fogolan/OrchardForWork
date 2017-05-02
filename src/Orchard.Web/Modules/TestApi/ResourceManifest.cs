using Orchard.UI.Resources;

namespace TestApi
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineStyle("TestApi.Common").SetUrl("common.css");
            manifest.DefineStyle("TestApi.ShoppingCart").SetUrl("shoppingcart.css").SetDependencies("TestApi.Common");

            manifest.DefineScript("TestApi.BookItem").SetUrl("bookItem.js").SetDependencies("jQuery");
        }
    }
}