using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using TestApi.Models;

namespace TestApi.Handlers
{
    public class ProductPartHandler : ContentHandler
    {
        public ProductPartHandler(IRepository<BookPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}