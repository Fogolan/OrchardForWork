using System.Collections.Generic;
using System.Linq;
using AllowedRoles.Models;
using Orchard.Caching;
using Orchard.Data;

namespace AllowedRoles.Services
{
    public class AllowedRoleService : IAllowedRoleService {
        private const string SignalName = "AllowedRoles.Services.AllowedRoleService";

        private readonly IRepository<AllowedRoleRecord> _allowedRolesRepository;
        private readonly ISignals _signals;

        public AllowedRoleService(
            IRepository<AllowedRoleRecord> allowedRoleRepository,
            ISignals signals) {
            _allowedRolesRepository = allowedRoleRepository;
            _signals = signals;
        }

        public void CreateAllowedRoleForRole( string roleName, string allowedRoleName) {
            if (_allowedRolesRepository.Get(x => x.AllowedRoleName == allowedRoleName && x.RoleName == roleName) == null)
            {
                _allowedRolesRepository.Create(new AllowedRoleRecord
                {
                    AllowedRoleName = allowedRoleName,
                    RoleName = roleName
                });
            }

            TriggerSignal();
        }

        public void CreateAllowedRolesForRole(string roleName, IList<string> allowedRoles)
        {
            foreach (var allowedRole in allowedRoles)
                CreateAllowedRoleForRole(roleName, allowedRole);
        }

        public IList<string> GetAllowedRolesForRoleByName(string roleName) {
            return _allowedRolesRepository.Fetch(x => x.RoleName == roleName).Select(x => x.AllowedRoleName).ToList();
        }

        private void TriggerSignal()
        {
            _signals.Trigger(SignalName);
        }
    }
}