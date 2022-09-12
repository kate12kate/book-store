using BookShop.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Web.Models.DTO
{
    public class AddToShoppingCartDto
    {
        public Book SelectedBook { get; set; }

        public Guid BookId { get; set; }

        public int Quantity { get; set; }
    }
}
