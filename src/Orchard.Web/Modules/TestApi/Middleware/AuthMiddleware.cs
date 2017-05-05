using System;
using System.Collections.Generic;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Orchard;
using Orchard.Owin;
using Orchard.Security;
using Owin;
using TestApi.Providers;

namespace TestApi.Middleware
{
    public class AuthMiddleware : IOwinMiddlewareProvider
    {
        private readonly IWorkContextAccessor _workContextAccessor;

        public AuthMiddleware(IWorkContextAccessor workContextAccessor)
        {
            _workContextAccessor = workContextAccessor;
        }

        public IEnumerable<OwinMiddlewareRegistration> GetOwinMiddlewares()
        {
            return new[] {
                new OwinMiddlewareRegistration {
                    Priority = "1",
                    Configure = app => {
                        var oAuthOptions = new OAuthAuthorizationServerOptions
                        {
                            TokenEndpointPath = new PathString("/Token"),
                            Provider = new AuthProvider(_workContextAccessor),
                            AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                            AllowInsecureHttp = true
                        };

                        app.UseOAuthAuthorizationServer(oAuthOptions);
                        app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
                    }
                }
            };
        }
    }
}