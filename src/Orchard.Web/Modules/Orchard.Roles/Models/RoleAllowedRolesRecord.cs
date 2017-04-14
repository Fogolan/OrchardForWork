using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orchard.Roles.Models
{
    public class RoleAllowedRolesRecord
    {
        public virtual int Id { get; set; }
        public virtual RoleRecord Role { get; set; }
        public virtual IList<AllowedRoleRecord> AllowedRoles { get; set; }

        public RoleAllowedRolesRecord() {
            AllowedRoles = new List<AllowedRoleRecord>();
        }
    }
}