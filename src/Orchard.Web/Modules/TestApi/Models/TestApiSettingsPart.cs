using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;

namespace TestApi.Models
{
    public class TestApiSettingsPart : ContentPart
    {
        public string SecretKey
        {
            get { return this.Retrieve(x => x.SecretKey); }
            set { this.Store(x => x.SecretKey, value); }
        }
    }
}