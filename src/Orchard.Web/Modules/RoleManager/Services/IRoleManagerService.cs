using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleManager.Services
{
    public interface IRoleManagerService
    {
        void CreateAllowedRoleForRole(string allowedRoleName, string roleName);
        void CreateAllowedRolesForRole(string roleName, IList<string> allowedRoles);
        IList<string> GetAllowedRolesForRoleByName(string roleName);
    }
}
