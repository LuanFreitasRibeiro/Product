using ProductCatalog.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(Guid id);
    }
}
