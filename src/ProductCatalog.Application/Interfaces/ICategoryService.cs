using ProductCatalog.Domain;
using ProductCatalog.Domain.Request.Category;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Service.Abstraction
{
    public interface ICategoryService
    {
        Task<Category> AddAsync(CreateCategoryRequest category);
        Task<Category> GetCategoryByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllCategoryAsync();
        Task DeleteCategoryAsync(Guid id);
        Task<Category> UpdateCategoryAsync(Guid id, UpdateCategoryRequest obj);
    }
}
