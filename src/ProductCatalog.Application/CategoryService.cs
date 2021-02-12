using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Service.Abstraction;
using ProductCatalog.Domain;
using ProductCatalog.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> AddAsync(Category category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                throw new ArgumentNullException("The name field is required");
            }

            var obj = new Category()
            {
                Id = Guid.NewGuid(),
                Name = category.Name,
            };

            await _categoryRepository.AddAsync(obj);

            return obj;
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }
        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _categoryRepository.GetAllCategoryAsync();
        }
        public async Task DeleteCategoryAsync(Guid id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<Category> UpdateCategoryAsync(Guid id, Category category)
        {
            try
            {
                var obj = new Category()
                {
                    Id = id,
                    Name = category.Name
                };

                await _categoryRepository.UpdateCategoryAsync(obj);
                return await _categoryRepository.GetCategoryByIdAsync(obj.Id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
