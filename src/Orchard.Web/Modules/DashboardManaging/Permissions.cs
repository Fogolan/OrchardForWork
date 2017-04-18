using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;

namespace DashboardManaging
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission TestPermission = new Permission { Description = "Visible custom menu Item", Name = "TestPermission" };
        public static readonly Permission SecondTestPermission = new Permission {Description = "Visible second custom menu Item", Name = "SecondTestPermission"};

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] {
                TestPermission, SecondTestPermission
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] {TestPermission, SecondTestPermission}
                },
            };
        }

    }
}