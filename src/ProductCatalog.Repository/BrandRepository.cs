using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Domain;
using ProductCatalog.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly StoreDataContext _context;

        public BrandRepository(StoreDataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Brand brand)
        {
            try
            {
                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<Brand>> GetAllBrandAsync()
        {
            return await _context.Brands
                .Select(x => new Brand
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<Brand> GetBrandByIdAsync(Guid id)
        {
            return await _context.Brands
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstAsync();
        }

        public async Task UpdateBrandAsync(Brand brand)
        {
            try
            {
                _context.Entry<Brand>(brand).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error has occurred");
            }
        }

        public async Task DeleteBrandAsync(Guid id)
        {
            try
            {
                Brand obj = _context.Brands.Find(id);
                _context.Entry<Brand>(obj).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {

                throw new ArgumentNullException(ex.Message);
            }
        }
    }
}
