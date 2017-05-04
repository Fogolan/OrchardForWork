using System;
using System.Collections.Generic;
using System.Linq;
using Orchard.Data;
using System.Web;
using Orchard;
using Orchard.ContentManagement;
using TestApi.Models;

namespace TestApi.Services
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IContentManager _contentManager;
        public IEnumerable<ShoppingCartItem> Items { get { return ItemsInternal.AsReadOnly(); } }

        private HttpContextBase HttpContext
        {
            get { return _workContextAccessor.GetContext().HttpContext; }
        }

        private List<ShoppingCartItem> ItemsInternal
        {
            get
            {
                var items = (List<ShoppingCartItem>)HttpContext.Session["ShoppingCart"];

                if (items == null)
                {
                    items = new List<ShoppingCartItem>();
                    HttpContext.Session["ShoppingCart"] = items;
                }

                return items;
            }
        }

        public ShoppingCart(IWorkContextAccessor workContextAccessor, IContentManager contentManager)
        {
            _workContextAccessor = workContextAccessor;
            _contentManager = contentManager;
        }

        public void Add(int bookId) {
            var item = new ShoppingCartItem(bookId);
            ItemsInternal.Add(item);
        }

        public void Remove(int bookId)
        {
            var item = Items.FirstOrDefault(x => x.BookId == bookId);
            if (item == null)
                return;

            ItemsInternal.Remove(item);
        }

        public BookPart GetBook(int bookId)
        {
            return _contentManager.Get<BookPart>(bookId);
        }

        public IEnumerable<BookPart> GetBooks()
        {
            var ids = Items.Select(x => x.BookId).ToList();
            var productParts = _contentManager.GetMany<BookPart>(ids, VersionOptions.Latest, QueryHints.Empty);

            return productParts;
        }

        private void Clear() {
            ItemsInternal.Clear();
        }
    }
}