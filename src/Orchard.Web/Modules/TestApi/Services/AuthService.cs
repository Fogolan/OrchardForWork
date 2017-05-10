using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Orchard.Security;

namespace TestApi.Services
{
    public class AuthService : IAuthService
    {
        public Task GenerateTokenForUser(IUser user) {
            var identity = new ClaimsIdentity("Bearer");
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            AuthenticationTicket ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            var context = new OAuthAuthorizationServerOptions();
            string token = context.AccessTokenFormat.Protect(ticket);
            return Task.FromResult<object>(null);
        }
    }
}