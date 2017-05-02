using System;

namespace TestApi.Models
{
    [Serializable]
    public sealed class ShoppingCartItem
    {
        public int BookId { get; private set; }

        public ShoppingCartItem()
        {
        }

        public ShoppingCartItem(int bookId)
        {
            BookId = bookId;
        }
    }
}