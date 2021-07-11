using ProductCatalog.Domain;
using ProductCatalog.Domain.Request.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Service.Abstraction
{
    public interface IProductService
    {
        Task<Product> AddAsync(CreateProductRequest product);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task DeleteProductAsync(Guid id);
        Task<Product> UpdateProductAsync(Guid id, UpdateProductRequest obj);
    }
}
