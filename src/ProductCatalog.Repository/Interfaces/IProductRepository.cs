using ProductCatalog.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Repository.Abstraction
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task DeleteProductAync(Guid id);
        Task UpdateProductAsync(Product product);
    }
}
