using System.Collections.Generic;
using Orchard.Data.Conventions;

namespace Orchard.Roles.Models
{
    public class RoleAllowedRolesRecord
    {
        public virtual int Id { get; set; }
        public virtual RoleRecord Role { get; set; }
        public virtual AllowedRoleRecord AllowedRole { get; set; }
    }
}