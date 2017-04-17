using System.Collections.Generic;
using Orchard.Data.Conventions;

namespace Orchard.Roles.Models {
    public class RoleRecord {
        public RoleRecord() {
            RolesPermissions = new List<RolesPermissionsRecord>();
            AllowedRoles = new List<RoleAllowedRolesRecord>();
        }

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        [CascadeAllDeleteOrphan]
        public virtual IList<RolesPermissionsRecord> RolesPermissions { get; set; }
        [CascadeAllDeleteOrphan]
        public virtual IList<RoleAllowedRolesRecord> AllowedRoles { get; set; }
    }
}