using EBook.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBook.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteBookFromSoppingCart(string userId, Guid bookId);
        bool order(string userId);
    }
}