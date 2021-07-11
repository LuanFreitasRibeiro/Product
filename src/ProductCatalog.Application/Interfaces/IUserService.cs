using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Domain;
using ProductCatalog.Domain.Request.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(CreateUserRequest user);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(Guid id);
    }
}
