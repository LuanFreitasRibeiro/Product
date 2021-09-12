using ProductCatalog.Domain;
using ProductCatalog.Domain.Request.Product;
using ProductCatalog.Domain.Response;
using ProductCatalog.Domain.Response.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Service.Abstraction
{
    public interface IProductService
    {
        Task<CreateProductResponse> AddAsync(CreateProductRequest product);
        Task<GetProductResponse> GetProductByIdAsync(Guid id);
        Task<IEnumerable<GetProductResponse>> GetAllProductAsync();
        Task DeleteProductAsync(Guid id);
        Task<UpdateProductResponse> UpdateProductAsync(Guid id, UpdateProductRequest obj);
    }
}
