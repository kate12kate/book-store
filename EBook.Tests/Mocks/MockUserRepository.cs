using EBook.Domain.Identity;
using EBook.Repository.Interface;
using Moq;


namespace Tests.Mocks
{
    public class MockUserRepository
    {
        public static Mock<IUserRepository> GetMock()
        {
            var mock = new Mock<IUserRepository>();
            var users = new List<EShopAppUser>()
            {
                new EShopAppUser()
                {
                    Id = Guid.Parse("5226de4b-0d64-4be4-a00d-064e24c33ce3").ToString(),
                    Email = "user@test.com",
                    FirstName = "User1",
                    LastName = "User1",


                }

            };
            mock.Setup(x => x.GetAll()).Returns(users);
            mock.Setup(x => x.Insert(It.IsAny<EShopAppUser>())).Callback((EShopAppUser user) => users.Add(user));
            mock.Setup(x => x.Update(It.IsAny<EShopAppUser>())).Callback((EShopAppUser user) => users.Add(user));
            mock.Setup(x => x.Delete(It.IsAny<EShopAppUser>())).Callback((EShopAppUser user) => users.Remove(user));
            return mock;
        }
    }
}
