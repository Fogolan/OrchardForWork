using System.Collections.Generic;
using System.Linq;
using Orchard.Caching;
using Orchard.Data;
using TestApi.Models;

namespace TestApi.Services
{
    public class RefreshTokenService : IRefreshTokenService {
        private const string SignalName = "TestApi.Services.RefreshTokenService";

        private readonly IRepository<RefreshTokenRecord> _refreshTokenRepository;
        private readonly ISignals _signals;

        public RefreshTokenService(
            IRepository<RefreshTokenRecord> refreshTokenRepository,
            ISignals signals) {
            _refreshTokenRepository = refreshTokenRepository;
            _signals = signals;
        }

        public void AddRefreshToken(RefreshTokenRecord token) {
            var existingToken = _refreshTokenRepository.Fetch(r => r.UserName == token.UserName).SingleOrDefault();
            if (existingToken != null)
                _refreshTokenRepository.Delete(existingToken);
            _refreshTokenRepository.Create(token);
            TriggerSignal();
        }

        public void RemoveRefreshToken(string protectedTicket) {
            var refreshToken = _refreshTokenRepository.Fetch(x => x.ProtectedTicket == protectedTicket).SingleOrDefault();

            if(refreshToken != null)
                _refreshTokenRepository.Delete(refreshToken);
            TriggerSignal();
        }

        public void RemoveRefreshToken(RefreshTokenRecord refreshToken) {
            _refreshTokenRepository.Delete(refreshToken);
            TriggerSignal();
        }

        public RefreshTokenRecord FindRefreshToken(string protectedTicket) {
            var refreshToken = _refreshTokenRepository.Fetch(x => x.ProtectedTicket == protectedTicket).FirstOrDefault();
            return refreshToken;
        }

        public List<RefreshTokenRecord> GetAllRefreshTokens() {
            return _refreshTokenRepository.Table.ToList();
        }

        private void TriggerSignal()
        {
            _signals.Trigger(SignalName);
        }
    }
}