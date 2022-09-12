using EBook.Domain.DomainModels;
using System;

namespace EBook.Domain.Relations
{
    public class BookInOrder : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}