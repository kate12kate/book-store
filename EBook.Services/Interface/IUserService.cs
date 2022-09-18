using EBook.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook.Service.Interface
{
    public interface IUserService
    {
        public List<EShopAppUser> GetAllUsers();
    }
}
