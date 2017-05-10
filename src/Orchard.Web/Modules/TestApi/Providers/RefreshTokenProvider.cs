using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;

namespace TestApi.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            Create(context);
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            Receive(context);
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            object inputs;
            context.OwinContext.Environment.TryGetValue("Microsoft.Owin.Form#collection", out inputs);

            var formCollection = (FormCollection)inputs;
            if (formCollection != null) {
                var grant = formCollection.GetValues("grant_type").FirstOrDefault();

                if (grant == null || grant.Equals("refresh_token")) return;
            }

            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddDays(1);

            context.SetToken(context.SerializeTicket());
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);

            if (context.Ticket == null)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                context.Response.ReasonPhrase = "invalid token";
                return;
            }

            if (context.Ticket.Properties.ExpiresUtc <= DateTime.UtcNow)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                context.Response.ReasonPhrase = "unauthorized";
                return;
            }

            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddDays(1);
            context.SetTicket(context.Ticket);
        }
    }
}