using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Domain;
using ProductCatalog.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDataContext _context;

        public CategoryRepository(StoreDataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories
                .Select(x => new Category
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _context.Categories
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            try
            {
                _context.Entry<Category>(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error has occurred");
            }
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            try
            {
                Category obj = _context.Categories.Find(id);
                _context.Entry<Category>(obj).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
