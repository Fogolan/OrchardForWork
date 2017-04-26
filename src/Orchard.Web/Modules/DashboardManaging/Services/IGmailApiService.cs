using Google.Apis.Auth.OAuth2;

namespace DashboardManaging.Services
{
    public interface IGmailApiService {
        UserCredential GetUserCredential();
    }
}
