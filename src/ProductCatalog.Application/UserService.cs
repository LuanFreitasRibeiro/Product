using AutoMapper;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Domain;
using ProductCatalog.Domain.Request.User;
using ProductCatalog.Domain.Response.User;
using ProductCatalog.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ProductCatalog.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest user)
        {
            Hash hash = new Hash(SHA256.Create());

            var obj = new User()
            {
                Id = Guid.NewGuid(),
                Username = user.Username,
                Password = hash.EncryptPassword(user.Password),
                Role = user.Role
            };

            await _userRepository.Add(obj);

            var result = _mapper.Map<CreateUserResponse>(obj);

            return result;
        }

        public async Task<GetUserResponse> GetUserById(Guid id)
        {
            var response = await _userRepository.GetUserById(id);

            var result = _mapper.Map<GetUserResponse>(response);

            return result;
        }

        public async Task<IEnumerable<GetUserResponse>> GetUsers()
        {
            var response = await _userRepository.GetUsers();

            var result = _mapper.Map<IEnumerable<GetUserResponse>>(response);

            return result;
        }
    }
}
