using ProductCatalog.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Repository.Abstraction
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category brand);
        Task<Category> GetCategoryByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllCategoryAsync();
        Task DeleteCategoryAsync(Guid id);
        Task UpdateCategoryAsync(Category category);
    }
}
