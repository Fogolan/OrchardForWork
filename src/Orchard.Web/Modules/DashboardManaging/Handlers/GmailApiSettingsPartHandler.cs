using DashboardManaging.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;

namespace DashboardManaging.Handlers
{
    public class GmailApiSettingsPartHandler : ContentHandler
    {
        public GmailApiSettingsPartHandler() {
            T = NullLocalizer.Instance;
            Filters.Add(new ActivatingFilter<GmailApiSettingsPart>("Site"));
            Filters.Add(new TemplateFilterForPart<GmailApiSettingsPart>("GmailApiSettings", "Parts/GmailApiSettings", "GmailApi"));
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            if (context.ContentItem.ContentType != "Site") {
                return;
            }
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("GmailApi")));
        }
    }
}