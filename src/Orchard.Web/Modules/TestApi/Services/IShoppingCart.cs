using System.Collections.Generic;
using Orchard;
using TestApi.Models;

namespace TestApi.Services
{
    public interface IShoppingCart : IDependency
    {
        IEnumerable<ShoppingCartItem> Items { get; }
        void Add(int bookId);
        void Remove(int bookId);
        BookPart GetBook(int bookId);
        IEnumerable<BookPart> GetBooks();
    }
}
