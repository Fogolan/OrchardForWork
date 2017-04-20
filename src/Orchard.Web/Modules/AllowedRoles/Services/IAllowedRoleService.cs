using System.Collections.Generic;
using Orchard;

namespace AllowedRoles.Services
{
    public interface IAllowedRoleService : IDependency
    {
        void CreateAllowedRoleForRole(string roleName, string allowedRoleName);
        void CreateAllowedRolesForRole(string roleName, IList<string> allowedRoles);
        IList<string> GetAllowedRolesForRoleByName(string roleName);
    }
}