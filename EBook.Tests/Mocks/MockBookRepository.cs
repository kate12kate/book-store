using EBook.Domain.DomainModels;
using EBook.Repository.Interface;
using Moq;

namespace Tests.Mocks
{
    public class MockBookRepository
    {
        public static Mock<IRepository<Book>> GetMock()
        {
            var mock = new Mock<IRepository<Book>>();

            var books = new List<Book>()
            {
                new Book()
                {
                    Id= Guid.Parse("03a31787-a173-4160-a21c-3c5b8aa4b9f1"),
                    BookName="Book1",
                    BookImage="Book1.jpg",
                    BookDescription="Book1 Description",
                    Price=100,

                }
            };
            mock.Setup(x => x.GetAll()).Returns(books);
            //mock.Setup(x => x.Get(It.IsAny<Guid>)).Returns((Guid id)=>books.FirstOrDefault(o=>o.Id==id));
            mock.Setup(x => x.Insert(It.IsAny<Book>())).Callback((Book book) => books.Add(book));
            mock.Setup(x => x.Update(It.IsAny<Book>())).Callback((Book book) => books.Add(book));
            mock.Setup(x => x.Delete(It.IsAny<Book>())).Callback((Book book) => books.Remove(book));
            return mock;




        }
    }
}
