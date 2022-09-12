


using EBook.Domain;
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
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<BookInOrder> _bookInOrderRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<BookInOrder> bookInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _bookInOrderRepository = bookInOrderRepository;
        }


        public bool deleteBookFromSoppingCart(string userId, Guid bookId)
        {
            if (!string.IsNullOrEmpty(userId) && bookId != null)
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.BookInShoppingCarts.Where(z => z.BookId.Equals(bookId)).FirstOrDefault();

                userShoppingCart.BookInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }
            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCard = loggedInUser.UserCart;

                var allBooks = userCard.BookInShoppingCarts.ToList();

                var allBookPrices = allBooks.Select(z => new
                {
                    BookPrice = z.CurrnetBook.Price,
                    Quantity = z.Quantity
                }).ToList();

                double totalPrice = 0.0;

                foreach (var item in allBookPrices)
                {
                    totalPrice += item.Quantity * item.BookPrice;
                }

                var reuslt = new ShoppingCartDto
                {
                    Books =allBooks,
                    TotalPrice = totalPrice
                };

                return reuslt;
            }
            return new ShoppingCartDto();
        }

        public bool order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCard = loggedInUser.UserCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<BookInOrder> bookInOrders = new List<BookInOrder>();

                var result = userCard.BookInShoppingCarts.Select(z => new BookInOrder
                {
                    Id = Guid.NewGuid(),
                    BookId = z.CurrnetBook.Id,
                    Book = z.CurrnetBook,
                    OrderId = order.Id,
                    Order = order
                }).ToList();

                bookInOrders.AddRange(result);

                foreach (var item in bookInOrders)
                {
                    this._bookInOrderRepository.Insert(item);
                }

                loggedInUser.UserCart.BookInShoppingCarts.Clear();

                this._userRepository.Update(loggedInUser);

                return true;
            }

            return false;
        }
    }
}