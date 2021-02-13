using ProductCatalog.Application.Service.Abstraction;
using ProductCatalog.Domain;
using ProductCatalog.Repository.Abstraction;
using ProductCatalog.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> AddAsync(Product product)
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

                return obj;
            }
            catch (ArgumentException ex)
            {

                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _productRepository.DeleteProductAync(id);
        }

        public async Task<Product> UpdateProductAsync(Guid id, EditorProductViewModel product)
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
            return await _productRepository.GetProductByIdAsync(id);
        }
    }
}
