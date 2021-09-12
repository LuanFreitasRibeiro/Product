using AutoMapper;
using ProductCatalog.Application.Service.Abstraction;
using ProductCatalog.Application.Validators;
using ProductCatalog.Domain;
using ProductCatalog.Domain.Request.Brand;
using ProductCatalog.Domain.Response;
using ProductCatalog.Domain.Response.Brand;
using ProductCatalog.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<CreateBrandResponse> AddAsync(CreateBrandRequest brand)
        {
            var obj = new Brand()
            {
                Id = Guid.NewGuid(),
                Name = brand.Name,
            };

            await _brandRepository.AddAsync(obj);

            var result = _mapper.Map<CreateBrandResponse>(obj);

            return result;
        }

        public async Task<GetBrandResponse> GetBrandByIdAsync(Guid id)
        {
            var response = await _brandRepository.GetBrandByIdAsync(id);

            var result = _mapper.Map<GetBrandResponse>(response);

            return result;
        }
        public async Task<IEnumerable<GetBrandResponse>> GetAllBrandAsync()
        {
            var response = await _brandRepository.GetAllBrandAsync();

            var result = _mapper.Map<IEnumerable<GetBrandResponse>>(response);
            return result;
        }
        public async Task DeleteBrandAsync(Guid id)
        {
            await _brandRepository.DeleteBrandAsync(id);
        }

        public async Task<UpdateBrandResponse> UpdateBrandAsync(Guid id, UpdateBrandRequest brand)
        {
            try
            {
                var obj = new Brand()
                {
                    Id = id,
                    Name = brand.Name
                };

                await _brandRepository.UpdateBrandAsync(obj);

                var response = await _brandRepository.GetBrandByIdAsync(obj.Id);

                var result = _mapper.Map<UpdateBrandResponse>(response);

                return result;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("An error has occurred");
            }
        }
    }
}
