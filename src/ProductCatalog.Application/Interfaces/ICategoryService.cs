using ProductCatalog.Domain;
using ProductCatalog.Domain.Request.Category;
using ProductCatalog.Domain.Response;
using ProductCatalog.Domain.Response.Category;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Service.Abstraction
{
    public interface ICategoryService
    {
        Task<CreateCategoryResponse> AddAsync(CreateCategoryRequest category);
        Task<GetCategoryResponse> GetCategoryByIdAsync(Guid id);
        Task<IEnumerable<GetCategoryResponse>> GetAllCategoryAsync();
        Task DeleteCategoryAsync(Guid id);
        Task<UpdateCategoryResponse> UpdateCategoryAsync(Guid id, UpdateCategoryRequest obj);
    }
}
