using BookShop.Web.Controllers;
using EBook.Domain.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tests.Mocks;


namespace Tests
{
    public class BookControllerTests
    {
        [Fact]
        public void Test_ReturnAllBooks()
        {
            // Arrange
            var mock = MockBookService.GetMock();

            var controller = new BooksController(mock.Object);

            //Act
            var result = controller.Index();

            //Assert
            mock.Verify(r => r.GetAllBooks());
            Assert.IsType<ViewResult>(result);
            //Assert.NotNull(result);
            //Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            //Assert.IsAssignableFrom<IEnumerable<Book>>(result.Value);
            //Assert.NotEmpty(result.Value as IEnumerable<Book>);

        }
    }

}

