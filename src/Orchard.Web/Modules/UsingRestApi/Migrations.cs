using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace UsingRestApi
{
    public class Migrations : DataMigrationImpl
    {
        public int Create() {
            SchemaBuilder.CreateTable("ProductPartRecord", table => table
                .ContentPartRecord()
                .Column<decimal>("UnitPrice")
                .Column<string>("Sku", column => column.WithLength(50))
            );
            ContentDefinitionManager.AlterPartDefinition("ProductPart", part => part
                .Attachable());
            return 1;
        }
    }
}