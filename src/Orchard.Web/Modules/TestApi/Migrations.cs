using System;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace TestApi
{
    public class Migrations : DataMigrationImpl
    {
        public int Create() {
            return 1;
        }

        public int UpdateFrom1() {
            SchemaBuilder.CreateTable("BookPartRecord", table => table
                .ContentPartRecord()
                .Column<string>("Name", colomn => colomn.WithLength(50))
                .Column<string>("Author", colomn => colomn.WithLength(50))
                .Column<int>("Year"));

            return 2;
        }

        public int UpdateFrom2() {
            ContentDefinitionManager.AlterPartDefinition("BookPart", part => part
                .Attachable());

            return 3;
        }

        public int UpdateFrom3() {
            SchemaBuilder.CreateTable("WebServiceSettingsPartRecord",
                table => table.ContentPartVersionRecord()
                    .Column<string>("ApiKey")
                    .Column<bool>("IsActive")
            );
            return 4;
        }

        public int UpdateFrom4() {
            SchemaBuilder.CreateTable("RefreshTokenRecord", table => table
                .Column<int>("Id", column => column.PrimaryKey().Identity())
                .Column<string>("Token", c => c.NotNull().WithLength(500))
                .Column<string>("UserName", c => c.NotNull())
                .Column<DateTime>("IssuedUtc", c => c.NotNull())
                .Column<DateTime>("ExpiresUtc", c => c.NotNull())
                .Column<string>("ProtectedTicket", c => c.NotNull().WithLength(500))
            );
            return 5;
        }
    }
}