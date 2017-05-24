using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using TestApi.Middleware;
using TestApi.Services;

namespace TestApi.Providers {
    public class RefreshTokenProvider : IAuthenticationTokenProvider {
        private readonly IRefreshTokenService _refreshTokenService;

        public RefreshTokenProvider(IRefreshTokenService refreshTokenService) {
            _refreshTokenService = refreshTokenService;
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context) {
            Create(context);
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context) {
            Receive(context);
        }

        public void Create(AuthenticationTokenCreateContext context) {
            object inputs;
            context.OwinContext.Environment.TryGetValue("Microsoft.Owin.Form#collection", out inputs);

            var formCollection = (FormCollection) inputs;
            if (formCollection != null) {
                var grant = formCollection.GetValues("grant_type").FirstOrDefault();

                if (grant == null || grant.Equals("refresh_token")) return;
            }

            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddDays(1);

            context.SetToken(context.SerializeTicket());
        }

        public void Receive(AuthenticationTokenReceiveContext context) {

            var protectedTicket = context.Token;
            var refreshToken = _refreshTokenService.FindRefreshToken(protectedTicket);
            if (refreshToken == null) return;
            var recieveContext = new AuthenticationTokenReceiveContext(context.OwinContext, AuthMiddleware.AuthBearerAuthenticationOptions.AccessTokenFormat, refreshToken.Token);
            recieveContext.DeserializeTicket(recieveContext.Token);
            _refreshTokenService.RemoveRefreshToken(protectedTicket);
            recieveContext.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddDays(1);
            context.SetTicket(recieveContext.Ticket);
        }
    }
}