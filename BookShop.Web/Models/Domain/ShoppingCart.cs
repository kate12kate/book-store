using BookShop.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Web.Models.Domain
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }

        public string OwnerId { get; set; }
        public virtual EShopAppUser Owner { get; set; }

        
        public virtual ICollection<BookInShoppingCart> BookInShoppingCarts { get; set; }
    }
}
