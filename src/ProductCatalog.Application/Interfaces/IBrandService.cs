using ProductCatalog.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Service.Abstraction
{
    public interface IBrandService
    {
        Task<Brand> AddAsync(Brand brand);
        Task<Brand> GetBrandByIdAsync(Guid id);
        Task<IEnumerable<Brand>> GetAllBrandAsync();
        Task DeleteBrandAsync(Guid id);
        Task<Brand> UpdateBrandAsync(Guid id, Brand obj);
    }
}
