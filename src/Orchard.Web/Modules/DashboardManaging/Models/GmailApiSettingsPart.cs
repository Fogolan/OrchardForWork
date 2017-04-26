using Orchard.ContentManagement;

namespace DashboardManaging.Models
{
    public class GmailApiSettingsPart : ContentPart
    {
        public string ClientId
        {
            get { return this.Retrieve(x => x.ClientId); }
            set { this.Store(x => x.ClientId, value); }
        }

        public string DiscoveryDocs
        {
            get { return this.Retrieve(x => x.DiscoveryDocs); }
            set { this.Store(x => x.DiscoveryDocs, value); }
        }

        public string Scopes
        {
            get { return this.Retrieve(x => x.Scopes); }
            set { this.Store(x => x.Scopes, value); }
        }
    }
}