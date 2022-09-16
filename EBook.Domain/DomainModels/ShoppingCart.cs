using EBook.Domain;

using EBook.Domain.Identity;
using EBook.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBook.Domain
{
    public class ShoppingCart:BaseEntity
    {
       

        public string OwnerId { get; set; }
        public virtual EShopAppUser Owner { get; set; }

        
        public virtual ICollection<BookInShoppingCart> BookInShoppingCarts { get; set; }
    }
}
