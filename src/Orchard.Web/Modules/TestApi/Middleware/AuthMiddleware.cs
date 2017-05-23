using System;
using System.Collections.Generic;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Orchard;
using Orchard.Data;
using Orchard.Owin;
using Orchard.Security;
using Owin;
using TestApi.Models;
using TestApi.Providers;
using TestApi.Services;

namespace TestApi.Middleware
{
    public class AuthMiddleware : IOwinMiddlewareProvider {

        public static OAuthBearerAuthenticationOptions AuthBearerAuthenticationOptions;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IWorkContextAccessor _workContextAccessor;

        public AuthMiddleware(
            IWorkContextAccessor workContextAccessor,
            IRefreshTokenService refreshTokenService) {
            _workContextAccessor = workContextAccessor;
            _refreshTokenService = refreshTokenService;
        }

        static AuthMiddleware() {
            AuthBearerAuthenticationOptions = new OAuthBearerAuthenticationOptions();
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
                            AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(30),
                            AllowInsecureHttp = true,
                            RefreshTokenProvider = new RefreshTokenProvider(_refreshTokenService)
                        };

                        app.UseOAuthAuthorizationServer(oAuthOptions);
                        app.UseOAuthBearerAuthentication(AuthBearerAuthenticationOptions);
                    }
                }
            };
        }
    }
}