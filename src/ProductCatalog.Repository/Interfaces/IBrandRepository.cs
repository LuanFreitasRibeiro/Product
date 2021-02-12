using ProductCatalog.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Repository.Abstraction
{
    public interface IBrandRepository
    {
        Task AddAsync(Brand brand);
        Task<Brand> GetBrandByIdAsync(Guid id);
        Task<IEnumerable<Brand>> GetAllBrandAsync();
        Task DeleteBrandAsync(Guid id);
        Task UpdateBrandAsync(Brand id);
    }
}
