using Microsoft.Owin;
using Newtonsoft.Json.Linq;
using Orchard;

namespace TestApi.Services
{
    public interface IAuthService : IDependency {
        string GenerateLocalAccessTokenResponse(string userName);
    }
}
