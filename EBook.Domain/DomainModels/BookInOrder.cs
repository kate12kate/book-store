using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Domain.DomainModels
{
    public class BookInOrder : BaseEntity
    {
        public Guid BookId { get; set; }
        public Book Book { get; set; }

        public Guid OrderId { get; set; }
        public Order UserOrder { get; set; }
        public int Quantity { get; set; }
    }
}
