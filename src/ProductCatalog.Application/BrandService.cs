using ProductCatalog.Application.Service.Abstraction;
using ProductCatalog.Application.Validators;
using ProductCatalog.Domain;
using ProductCatalog.Repository.Abstraction;
using ProductCatalog.ViewModels.BrandsViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly BrandValidator _brandValidator;

        public BrandService(IBrandRepository brandRepository, BrandValidator brandValidator)
        {
            _brandRepository = brandRepository;
            _brandValidator = brandValidator;
        }

        public async Task<Brand> AddAsync(Brand brand)
        {
            _brandValidator.ValidateIfNameIsNullOrEmpty(brand);
            //_brandValidator.ValidateIfNameExists(brand);

            var obj = new Brand()
            {
                Id = Guid.NewGuid(),
                Name = brand.Name,
            };

            await _brandRepository.AddAsync(obj);

            return obj;
        }

        public async Task<Brand> GetBrandByIdAsync(Guid id)
        {
            return await _brandRepository.GetBrandByIdAsync(id);
        }
        public async Task<IEnumerable<Brand>> GetAllBrandAsync()
        {
            return await _brandRepository.GetAllBrandAsync();
        }
        public async Task DeleteBrandAsync(Guid id)
        {
            await _brandRepository.DeleteBrandAsync(id);
        }

        public async Task<Brand> UpdateBrandAsync(Guid id, EditorBrandViewModel brand)
        {
            try
            {
                var obj = new Brand()
                {
                    Id = id,
                    Name = brand.Name
                };

                await _brandRepository.UpdateBrandAsync(obj);
                return await _brandRepository.GetBrandByIdAsync(obj.Id);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("An error has occurred");
            }
        }
    }
}
