

using EBook.Domain.DomainModels;
using EBook.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBook.Service.Interface
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book GetDetailsForBook(Guid? id);
        void CreateNewBook(Book p);
        void UpdeteExistingBook(Book p);
        AddToShoppingCartDto GetShoppingCartInfo(Guid? id);
        void DeleteBook(Guid id);
        bool AddToShoppingCart(AddToShoppingCartDto item, string userID);
    }
}