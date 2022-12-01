using EBook.Domain.DomainModels;
using EBook.Service.Implementation;
using EBook.Service.Interface;
using Moq;


namespace Tests.Mocks
{
    public class MockBookService
    {
          public static Mock<IBookService> GetMock()
        {
            var mockBook = MockBookRepository.GetMock();
            var mockUser = MockUserRepository.GetMock();
            var mockbookInShoppingCart = MockBooksInShoppingCartRepository.GetMock();
            var bookService = new BookService(mockBook.Object, mockbookInShoppingCart.Object, mockUser.Object);
            var mock = new Mock<IBookService>();
            mock.Setup(x => x.GetAllBooks()).Returns(bookService.GetAllBooks());
            mock.Setup(x => x.GetDetailsForBook(It.IsAny<Guid>())).Returns(bookService.GetDetailsForBook(It.IsAny<Guid>()));
            //mock.Setup(x => x.GetAllBooksGenre(It.IsAny<string>())).Returns(bookService.GetAllBooksGenre(It.IsAny<string>()));
            // mock.Setup(x => x.CreateNewBook(It.IsAny<Book>())).Returns(bookService.CreateNewBook(It.IsAny<Book>()));
            return mock;
        }
    }
}
