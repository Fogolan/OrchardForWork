using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;
using TestApi.Models;

namespace TestApi.Handlers
{
    public class TestApiSettingsPartHandler : ContentHandler
    {
        public TestApiSettingsPartHandler() {
            T = NullLocalizer.Instance;
            Filters.Add(new ActivatingFilter<TestApiSettingsPart>("Site"));
            Filters.Add(new TemplateFilterForPart<TestApiSettingsPart>("TestApiSettings", "Parts/TestApiSettings", "TestApi"));
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("TestApi")));
        }
    }
}