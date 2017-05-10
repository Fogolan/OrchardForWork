using System.Threading.Tasks;
using Orchard;
using Orchard.Security;

namespace TestApi.Services
{
    public interface IAuthService : IDependency {
        Task GenerateTokenForUser(IUser user);
    }
}
