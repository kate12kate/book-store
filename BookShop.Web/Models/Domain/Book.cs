using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Web.Models.Domain
{
    public class Book
    {
        public Guid Id { get; set; }

        [Required]
        public string BookName { get; set; }
        [Required]
        public string BookImage { get; set; }
        [Required]
        public string BookDescription { get; set; }
        [Required]
        public int Price { get; set; }
        public int Rating { get; set; }

        public virtual ICollection<BookInShoppingCart> BookInShoppingCarts { get; set; }


    }
}
