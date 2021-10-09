using ProductCatalog.Domain.Request.User;
using ProductCatalog.Domain.Response.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Interfaces
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUser(CreateUserRequest user);
        Task<IEnumerable<GetUserResponse>> GetUsers();
        Task<GetUserResponse> GetUserById(Guid id);
    }
}
