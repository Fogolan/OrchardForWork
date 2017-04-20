using AllowedRoles.Services;
using Orchard.Data.Migration;

namespace AllowedRoles
{
    public class Migrations : DataMigrationImpl {
        private readonly IAllowedRoleService _allowedRoleService;

        public Migrations(IAllowedRoleService allowedRoleService) {
            _allowedRoleService = allowedRoleService;
        }

        public int Create()
        {
            SchemaBuilder.CreateTable("AllowedRoleRecord",
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("AllowedRoleName")
                    .Column<string>("RoleName")
            );

            return 1;
        }
    }
}