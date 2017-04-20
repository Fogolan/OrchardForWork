using System.Linq;
using AllowedRoles.Services;
using Orchard.Data.Migration;
using Orchard.Roles.Services;

namespace Orchard.Roles {
    public class RolesDataMigration : DataMigrationImpl {
        private readonly IRoleService _roleService;
        private readonly IAllowedRoleService _allowedRoleService;

        public RolesDataMigration(
            IRoleService roleService,
            IAllowedRoleService allowedRoleService) {
            _roleService = roleService;
            _allowedRoleService = allowedRoleService;
        }

        public int Create() {
            SchemaBuilder.CreateTable("PermissionRecord", 
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("Name")
                    .Column<string>("FeatureName")
                    .Column<string>("Description")
                );

            SchemaBuilder.CreateTable("RoleRecord", 
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("Name")
                );

            SchemaBuilder.CreateTable("RolesPermissionsRecord", 
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<int>("Role_id")
                    .Column<int>("Permission_id")
                    .Column<int>("RoleRecord_Id")
                );

            SchemaBuilder.CreateTable("UserRolesPartRecord", 
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<int>("UserId")
                    .Column<int>("Role_id")
                );

            return 2;
        }

        public int UpdateFrom1() {

            // creates default permissions for Orchard v1.4 instances and earlier
            _roleService.CreatePermissionForRole("Anonymous", Orchard.Core.Contents.Permissions.ViewContent.Name);
            _roleService.CreatePermissionForRole("Authenticated", Orchard.Core.Contents.Permissions.ViewContent.Name);

            return 2;
        }

        public int UpdateFrom2() {
            _roleService.CreateRole("Testing");
            return 3;
        }

        public int UpdateFrom3() {
            SchemaBuilder.CreateTable("AllowedRoleRecord",
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("AllowedRole")
            );
            SchemaBuilder.CreateTable("RoleAllowedRolesRecord",
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<int>("Role_id")
                    .Column<int>("AllowedRole_id")
                    .Column<int>("RoleRecord_Id")
            );

            return 4;
        }

        public int UpdateFrom4() {
            _roleService.CreateRole("MyRole");
            _roleService.CreateRole("HisRole");
            _roleService.CreateRole("YourRole");
            _roleService.CreateRole("HiddenRole");

            _roleService.CreatePermissionForRole("MyRole", "AccessAdminPanel");
            _roleService.CreatePermissionForRole("MyRole", "ManageUsers");
            _roleService.CreatePermissionForRole("MyRole", "ManageRoles");
            _roleService.CreatePermissionForRole("MyRole", "AssignRoles");
            _roleService.CreatePermissionForRole("MyRole", "PublishContent");
            _roleService.CreatePermissionForRole("MyRole", "SecondTestPermission");
            _roleService.CreatePermissionForRole("HisRole", "AccessAdminPanel");
            _roleService.CreatePermissionForRole("HisRole", "TestPermission");
            return 5;
        }

        public int UpdateFrom5()
        {
            _allowedRoleService.CreateAllowedRoleForRole("MyRole", "HisRole");
            _allowedRoleService.CreateAllowedRoleForRole("MyRole", "YourRole");
            _allowedRoleService.CreateAllowedRoleForRole("MyRole", "MyRole");

            foreach (var roleRecord in _roleService.GetRoles().ToList())
            {
                _allowedRoleService.CreateAllowedRoleForRole("Administrator", roleRecord.Name);
            }
            return 6;
        }
    }
}