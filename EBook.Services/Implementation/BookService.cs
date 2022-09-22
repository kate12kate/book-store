




using EBook.Domain.DomainModels;
using EBook.Domain.DTO;
using EBook.Domain.Relations;
using EBook.Repository.Interface;
using EBook.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBook.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<BookInShoppingCart> _bookInShoppingCartRepository;
        private readonly IUserRepository _userRepository;

        public BookService(IRepository<Book> bookRepository, IRepository<BookInShoppingCart> bookInShoppingCartRepository, IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _bookInShoppingCartRepository = bookInShoppingCartRepository;
        }


        public bool AddToShoppingCart(AddToShoppingCartDto item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.UserCart;

            if (item.SelectedBookId != null && userShoppingCard != null)
            {
                var book = this.GetDetailsForBook(item.SelectedBookId);

                if (book != null)
                {
                    BookInShoppingCart itemToAdd = new BookInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        CurrnetBook = book,
                        BookId = book.Id,
                        UserCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    this._bookInShoppingCartRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewBook(Book p)
        {
            this._bookRepository.Insert(p);
        }

        public void DeleteBook(Guid id)
        {
            var book = this.GetDetailsForBook(id);
            this._bookRepository.Delete(book);
        }
        public List<Book> GetAllBooksGenre(string genre)
        {
            
            return this._bookRepository.GetAll().Where(book => book.Genre.Equals(genre)).ToList();
        }
        public List<Book> GetAllBooks()
        {
            return this._bookRepository.GetAll().ToList();
        }

        public Book GetDetailsForBook(Guid? id)
        {
            return this._bookRepository.Get(id);
        }

        public AddToShoppingCartDto GetShoppingCartInfo(Guid? id)
        {
            var book = this.GetDetailsForBook(id);
            AddToShoppingCartDto model = new AddToShoppingCartDto
            {
                SelectedBook = book,
                SelectedBookId = book.Id,
                Quantity = 1
            };

            return model;
        }

        public void UpdeteExistingBook(Book p)
        {
            this._bookRepository.Update(p);
        }
    }
}