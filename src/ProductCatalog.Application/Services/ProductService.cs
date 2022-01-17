using AutoMapper;
using ProductCatalog.Application.Service.Abstraction;
using ProductCatalog.Domain;
using ProductCatalog.Domain.Request.Product;
using ProductCatalog.Domain.Response;
using ProductCatalog.Domain.Response.Product;
using ProductCatalog.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CreateProductResponse> AddAsync(CreateProductRequest product)
        {
            try
            {
                var obj = new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Image = product.Image,
                    CreateDate = DateTime.Now,
                    CategoryId = product.CategoryId,
                    BrandId = product.BrandId
                };

                await _productRepository.AddAsync(obj);

                var result = _mapper.Map<CreateProductResponse>(obj);

                return result;
            }
            catch (ArgumentException ex)
            {

                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<IEnumerable<GetProductResponse>> GetAllProductAsync()
        {
            var response = await _productRepository.GetAllProductsAsync();

            var result = _mapper.Map<IEnumerable<GetProductResponse>>(response);

            return result;
        }

        public async Task<GetProductResponse> GetProductByIdAsync(Guid id)
        {
            var response = await _productRepository.GetProductByIdAsync(id);

            var result = _mapper.Map<GetProductResponse>(response);

            return result;
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _productRepository.DeleteProductAync(id);
        }

        public async Task<UpdateProductResponse> UpdateProductAsync(Guid id, UpdateProductRequest product)
        {
            var obj = new Product()
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                Image = product.Image,
                LastUpdateDate = DateTime.Now,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId
            };

            await _productRepository.UpdateProductAsync(obj);

            var response = await _productRepository.GetProductByIdAsync(id);

            var result = _mapper.Map<UpdateProductResponse>(response);

            return result;
        }
    }
}
