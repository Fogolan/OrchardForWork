using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Caching;
using Orchard.Data;
using Orchard.Roles.Models;
using Orchard.Roles.Services;

namespace RoleManager.Services
{
    public class RoleManagerService : IRoleManagerService
    {
        private const string SignalName = "Orchard.RoleManager.Services";

        private readonly IRepository<AllowedRoleRecord> _allowedRolesRepository;
        private readonly IRepository<RoleAllowedRolesRecord> _roleAllowedRolesRepository;
        private readonly ISignals _signals;
        private readonly IRoleService _roleService;

        public RoleManagerService(IRepository<AllowedRoleRecord> allowedRolesRepository, IRepository<RoleAllowedRolesRecord> roleAllowedRolesRepository, ISignals signals, IRoleService roleService) {
            _allowedRolesRepository = allowedRolesRepository;
            _roleAllowedRolesRepository = roleAllowedRolesRepository;
            _signals = signals;
            _roleService = roleService;
        }

        public void CreateAllowedRoleForRole(string allowedRoleName, string roleName) {
            if (_roleService.GetRoleByName(allowedRoleName) == null)
                return;
            CreateAllowedRole(allowedRoleName);
            RoleRecord roleRecord = _roleService.GetRoleByName(roleName);
            AllowedRoleRecord allowedRoleRecord = _allowedRolesRepository.Get(x => x.AllowedRole == allowedRoleName);
            roleRecord.AllowedRoles.Add(new RoleAllowedRolesRecord { AllowedRole = allowedRoleRecord, Role = roleRecord });
            TriggerSignal();
        }

        public void CreateAllowedRolesForRole(string roleName, IList<string> allowedRoles)
        {
            foreach (var allowedRole in allowedRoles)
                CreateAllowedRoleForRole(roleName, allowedRole);
        }

        public IList<string> GetAllowedRolesForRoleByName(string roleName) {
            return _roleAllowedRolesRepository.Fetch(x => x.Role.Name == roleName).Select(x => x.AllowedRole.AllowedRole).ToList();
        }

        private void CreateAllowedRole(string allowedRoleName)
        {
            if (_allowedRolesRepository.Get(x => x.AllowedRole == allowedRoleName) == null)
            {
                _allowedRolesRepository.Create(new AllowedRoleRecord
                {
                    AllowedRole = allowedRoleName
                });
            }
        }

        private void TriggerSignal()
        {
            _signals.Trigger(SignalName);
        }
    }
}