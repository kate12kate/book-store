using EBook.Domain.DomainModels;
using System;

namespace EBook.Domain.Relations
{
    public class BookInShoppingCart : BaseEntity
    {
        public Guid BookId { get; set; }
        public virtual Book CurrnetBook { get; set; }

        public Guid ShoppingCartId { get; set; }
        public virtual ShoppingCart UserCart { get; set; }

        public int Quantity { get; set; }
    }
}