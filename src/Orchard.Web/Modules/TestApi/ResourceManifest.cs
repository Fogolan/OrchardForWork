﻿using Orchard.UI.Resources;

namespace TestApi
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineStyle("TestApi.Common").SetUrl("common.css");
            manifest.DefineStyle("TestApi.ShoppingCart").SetUrl("shoppingcart.css").SetDependencies("TestApi.Common");

            manifest.DefineScript("TestApi.AddToCart").SetUrl("addToCart.js").SetDependencies("jQuery");
            manifest.DefineScript("TestApi.ShoppingCart").SetUrl("shoppingCart.js").SetDependencies("jQuery");
            manifest.DefineScript("TestApi.Register").SetUrl("registration.js").SetDependencies("jQuery");
            manifest.DefineScript("TestApi.RefreshToken").SetUrl("refreshToken.js").SetDependencies("jQuery");
            manifest.DefineScript("TestApi.login").SetUrl("login.js").SetDependencies("jQuery", "TestApi.RefreshToken");
        }
    }
}