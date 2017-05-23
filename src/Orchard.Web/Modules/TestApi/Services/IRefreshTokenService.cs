using System.Collections.Generic;
using Orchard;
using TestApi.Models;

namespace TestApi.Services
{
    public interface IRefreshTokenService : IDependency {
        void AddRefreshToken(RefreshTokenRecord token);
        void RemoveRefreshToken(RefreshTokenRecord refreshToken);
        void RemoveRefreshToken(string protectedTicket);
        RefreshTokenRecord FindRefreshToken(string refreshTokenId);
        List<RefreshTokenRecord> GetAllRefreshTokens();
    }
}
