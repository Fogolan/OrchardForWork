using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using TestApi.Middleware;
using System;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Newtonsoft.Json.Linq;
using TestApi.Models;

namespace TestApi.Services {
    public class AuthService : IAuthService {
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthService(IRefreshTokenService refreshTokenService) {
            _refreshTokenService = refreshTokenService;
        }

        public string GenerateLocalAccessTokenResponse(string login, IOwinContext owinContext) {
            var tokenExpiration = TimeSpan.FromSeconds(60);

            var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, login));

            var props = new AuthenticationProperties {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
                AllowRefresh = true
            };

            var ticket = new AuthenticationTicket(identity, props);
            var accessToken = AuthMiddleware.AuthBearerAuthenticationOptions.AccessTokenFormat.Protect(ticket);
            var refreshToken = GenerateRefreshToken(owinContext, ticket, login, accessToken);
            var tokenResponse = new {
                userName = login,
                access_token = accessToken,
                refresh_token = refreshToken,
                token_type = "bearer",
                expires_in = tokenExpiration.TotalSeconds.ToString(),
                issued = ticket.Properties.IssuedUtc.ToString(),
                expires = ticket.Properties.ExpiresUtc.ToString()
            };
            var token = Json.Encode(tokenResponse);
            
            owinContext.Response.Context.Environment.Add("access_key", token);
            return token;
        }

        public string GenerateRefreshToken(IOwinContext owinContext, AuthenticationTicket ticket, string userName, string accessToken) {
            var context = new AuthenticationTokenCreateContext(owinContext, AuthMiddleware.AuthBearerAuthenticationOptions.AccessTokenFormat, ticket);
            var token = new RefreshTokenRecord
            {
                Token = accessToken,
                UserName = userName,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddDays(1)
            };

            context.Ticket.Properties.IssuedUtc = DateTime.UtcNow;
            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddDays(1);
            var refreshToken = context.SerializeTicket();
            token.ProtectedTicket = refreshToken;
            context.SetToken(refreshToken);
            _refreshTokenService.AddRefreshToken(token);
            return refreshToken;
        }
    }
}