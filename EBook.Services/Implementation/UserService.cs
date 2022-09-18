using EBook.Domain.Identity;
using EBook.Repository.Interface;
using EBook.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
        }


        public List<EShopAppUser> GetAllUsers()
        {
            _logger.LogInformation("GetAllUsers was called!");
            return this._userRepository.GetAll().ToList();
        }

     
    }
}
