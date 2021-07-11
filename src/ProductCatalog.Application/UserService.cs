using ProductCatalog.Application.Interfaces;
using ProductCatalog.Domain;
using ProductCatalog.Domain.Request.User;
using ProductCatalog.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUser(CreateUserRequest user)
        {
            var obj = new User()
            {
                Id = Guid.NewGuid(),
                Username = user.Username,
                Password = user.Password,
                Role = user.Role
            };

            await _userRepository.Add(obj);

            obj.Password = "";

            return obj;
        }

        public async Task<User> GetUserById(Guid id)
        {
            var response = await _userRepository.GetUserById(id);

            response.Password = "";

            return response;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }
    }
}
