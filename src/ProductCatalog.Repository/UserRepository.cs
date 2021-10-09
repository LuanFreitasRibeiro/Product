using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Domain;
using ProductCatalog.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreDataContext _context;

        public UserRepository(StoreDataContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _context.Users
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstAsync();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users
                .Select(x => new User
                {
                    Id = x.Id,
                    Username = x.Username,
                    Role = x.Role
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
