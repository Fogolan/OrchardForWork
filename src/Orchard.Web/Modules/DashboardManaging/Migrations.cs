using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace DashboardManaging
{
    public class Migrations : DataMigrationImpl {

        public int Create()
        {
            ContentDefinitionManager.AlterPartDefinition("GmailApiPart", cfg => cfg
                .Attachable()
                .WithDescription("Gmail Api")
            );

            SchemaBuilder.CreateTable("GmailApiSettingsPartRecord",
                table => table.ContentPartVersionRecord()
                    .Column<string>("ClientId")
                    .Column<string>("DiscoveryDocs")
                    .Column<bool>("Scopes")
            );

            ContentDefinitionManager.AlterPartDefinition("GmailApiPart", builder =>
                builder.WithDescription("Gmail Api"));
            return 1;
        }
    }
}