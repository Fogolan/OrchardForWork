using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Roles.Models;
using Orchard.Roles.Services;
using Orchard.Security.Permissions;

namespace Orchard.RoleManager
{
    public class RoleManagerService : IRoleService
    {
        public IEnumerable<RoleRecord> GetRoles() {
            throw new NotImplementedException();
        }

        public RoleRecord GetRole(int id) {
            throw new NotImplementedException();
        }

        public RoleRecord GetRoleByName(string name) {
            throw new NotImplementedException();
        }

        public void CreateRole(string roleName) {
            if (GetRoleByName(roleName) != null)
                return;

            var roleRecord = new RoleRecord { Name = roleName };
            _roleRepository.Create(roleRecord);
            _roleEventHandlers.Created(new RoleCreatedContext { Role = roleRecord });
            TriggerSignal();
        }

        public void CreatePermissionForRole(string roleName, string permissionName) {
            throw new NotImplementedException();
        }

        public void UpdateRole(int id, string roleName, IEnumerable<string> rolePermissions) {
            throw new NotImplementedException();
        }

        public void DeleteRole(int id) {
            throw new NotImplementedException();
        }

        public IDictionary<string, IEnumerable<Permission>> GetInstalledPermissions() {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetPermissionsForRole(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetPermissionsForRoleByName(string name) {
            throw new NotImplementedException();
        }

        public bool VerifyRoleUnicity(string name) {
            throw new NotImplementedException();
        }
    }
}