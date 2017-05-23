using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using TestApi.Middleware;
using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Newtonsoft.Json.Linq;
using TestApi.Helpers;
using TestApi.Models;

namespace TestApi.Services {
    public class AuthService : IAuthService {
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthService(IRefreshTokenService refreshTokenService) {
            _refreshTokenService = refreshTokenService;
        }

        public JObject GenerateLocalAccessTokenResponse(string userName, IOwinContext owinContext) {
            var tokenExpiration = TimeSpan.FromSeconds(60);

            var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, userName));

            var props = new AuthenticationProperties {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
                AllowRefresh = true
            };

            var ticket = new AuthenticationTicket(identity, props);
            var accessToken = AuthMiddleware.AuthBearerAuthenticationOptions.AccessTokenFormat.Protect(ticket);
            var refreshToken = GenerateRefreshToken(owinContext, ticket, userName, accessToken);
            var tokenResponse = new JObject(
                new JProperty("userName", userName),
                new JProperty("access_token", accessToken),
                new JProperty("refresh_token", refreshToken),
                new JProperty("token_type", "bearer"),
                new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString())
            );

            //var recieveContext = new AuthenticationTokenReceiveContext(owinContext, AuthMiddleware.AuthBearerAuthenticationOptions.AccessTokenFormat, accessToken);
            //recieveContext.DeserializeTicket(recieveContext.Token);

            return tokenResponse;
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