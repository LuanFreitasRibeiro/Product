using ProductCatalog.Application.Service.Abstraction;
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

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<Brand> AddAsync(Brand brand)
        {
            if (string.IsNullOrEmpty(brand.Name))
            {
                throw new ArgumentNullException("The name field is required");
            }

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
