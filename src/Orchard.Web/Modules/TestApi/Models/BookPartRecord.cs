using System;
using Orchard.ContentManagement.Records;

namespace TestApi.Models
{
    public class BookPartRecord : ContentPartRecord
    {
        public virtual string Name { get; set; }
        public virtual string Author { get; set; }
        public virtual int Year { get; set; }
    }
}