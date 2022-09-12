using EBook.Domain.Identity;
using EBook.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBook.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public EShopAppUser User { get; set; }

        public virtual ICollection<BookInOrder> BookInOrders { get; set; }
    }
}
