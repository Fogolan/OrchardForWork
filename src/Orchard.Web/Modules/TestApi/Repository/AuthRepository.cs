using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchard;
using Orchard.Data;
using Orchard.Security;
using TestApi.Entities;

namespace TestApi.Repository
{
    public class AuthRepository : IDisposable
    {
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;

        public AuthRepository(IWorkContextAccessor workContextAccessor,
            IRepository<Client> clientRepository,
            IRepository<RefreshToken> refreshTokenRepository) {
            _workContextAccessor = workContextAccessor;
            _clientRepository = clientRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public IUser FindUser(string userName, string password)
        {
            var membershipService = _workContextAccessor.GetContext().Resolve<IMembershipService>();
            var user = membershipService.ValidateUser(userName, password);
            return user;
        }

        public Client FindClient(string clientId) {
            var client = _clientRepository.Fetch(x => x.Id == clientId).FirstOrDefault();
            return client;
        }

        public void AddRefreshToken(RefreshToken token)
        {

            var existingToken = _refreshTokenRepository.Get(r => r.Subject == token.Subject && r.ClientId == token.ClientId);

            if (existingToken != null)
            {
                RemoveRefreshToken(existingToken.Id);
            }

            _refreshTokenRepository.Create(token);
        }

        public void RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = _refreshTokenRepository.Fetch(x => x.Id == refreshTokenId).FirstOrDefault();
            if (refreshToken != null)
                _refreshTokenRepository.Delete(refreshToken);
        }

        public RefreshToken FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = _refreshTokenRepository.Fetch(x => x.Id == refreshTokenId).FirstOrDefault();

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _refreshTokenRepository.Fetch(x => true).ToList();
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) { }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}