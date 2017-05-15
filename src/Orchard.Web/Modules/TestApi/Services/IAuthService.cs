using Newtonsoft.Json.Linq;
using Orchard;

namespace TestApi.Services
{
    public interface IAuthService : IDependency {
        JObject GenerateLocalAccessTokenResponse(string userName);
    }
}
