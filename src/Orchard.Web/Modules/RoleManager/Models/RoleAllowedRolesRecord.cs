using Orchard.Roles.Models;

namespace RoleManager.Models
{
    public class RoleAllowedRolesRecord
    {
        public virtual int Id { get; set; }
        public virtual RoleRecord Role { get; set; }
        public virtual AllowedRoleRecord AllowedRole { get; set; }
    }
}