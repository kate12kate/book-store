
using EBook.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBook.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<EShopAppUser> GetAll();
        EShopAppUser Get(string id);
        void Insert(EShopAppUser entity);
        void Update(EShopAppUser entity);
        void Delete(EShopAppUser entity);
    }
}