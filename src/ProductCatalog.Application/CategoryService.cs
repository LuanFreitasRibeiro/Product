using AutoMapper;
using ProductCatalog.Application.Service.Abstraction;
using ProductCatalog.Domain;
using ProductCatalog.Domain.Request.Category;
using ProductCatalog.Domain.Response;
using ProductCatalog.Domain.Response.Category;
using ProductCatalog.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CreateCategoryResponse> AddAsync(CreateCategoryRequest category)
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

            var result = _mapper.Map<CreateCategoryResponse>(obj);

            return result;
        }

        public async Task<GetCategoryResponse> GetCategoryByIdAsync(Guid id)
        {
            var response = await _categoryRepository.GetCategoryByIdAsync(id);

            var result = _mapper.Map<GetCategoryResponse>(response);

            return  result;
        }
        public async Task<IEnumerable<GetCategoryResponse>> GetAllCategoryAsync()
        {
            var response = await _categoryRepository.GetAllCategoryAsync();

            var result = _mapper.Map<IEnumerable<GetCategoryResponse>>(response);

            return result;
        }
        public async Task DeleteCategoryAsync(Guid id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<UpdateCategoryResponse> UpdateCategoryAsync(Guid id, UpdateCategoryRequest category)
        {
            try
            {
                var obj = new Category()
                {
                    Id = id,
                    Name = category.Name
                };

                await _categoryRepository.UpdateCategoryAsync(obj);

                var response = await _categoryRepository.GetCategoryByIdAsync(obj.Id);

                var result = _mapper.Map<UpdateCategoryResponse>(response);

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
