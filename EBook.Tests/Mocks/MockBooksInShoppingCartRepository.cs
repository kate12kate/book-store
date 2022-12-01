
using EBook.Domain.Relations;
using EBook.Repository.Interface;
using Moq;

namespace Tests.Mocks
{
    public class MockBooksInShoppingCartRepository
    {
        public static Mock<IRepository<BookInShoppingCart>> GetMock()
        {
            var mock = new Mock<IRepository<BookInShoppingCart>>();



            var booksInShoppingCart = new List<BookInShoppingCart>()

            {
                new BookInShoppingCart()
                {
                    BookId = Guid.Parse("03a31787-a173-4160-a21c-3c5b8aa4b9f1"),
                    ShoppingCartId = Guid.NewGuid(),
                    Quantity = 1
                },
           
             };  
                mock.Setup(x => x.GetAll()).Returns(booksInShoppingCart);

            mock.Setup(x => x.Insert(It.IsAny<BookInShoppingCart>())).Callback((BookInShoppingCart bookInShoppingCart) => booksInShoppingCart.Add(bookInShoppingCart));
           
            return mock;
        }
    }
}
