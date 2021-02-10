using ProductCatalog_Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Repositories.Abstraction
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
