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
    }
}