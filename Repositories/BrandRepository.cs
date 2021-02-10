using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Repositories.Abstraction;
using ProductCatalog.ViewModels.BrandsViewModels;
using ProductCatalog_Domain;

namespace ProductCatalog.Repositories
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
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();

        }

        public async Task<Brand> GetBrandByIdAsync(Guid id)
        {
            return await _context.Brands.FindAsync(id);
        }

        public async Task<IEnumerable<Brand>> GetAllBrandAsync()
        {
            return await _context.Brands
                                 .AsQueryable()
                                 .ToListAsync();
        }

        public async Task DeleteBrandAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateBrandAsync(Brand id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListBrandViewModel> GetAllBrands()
        {
            return _context.Brands
                    .Select(x => new ListBrandViewModel
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                    .AsNoTracking()
                    .ToList();
        }

        public Brand GetBrandbyId(int id)
        {
            return _context.Brands.Find(id);
        }

        public IEnumerable<Product> GetProductsBrand(int id)
        {
            return _context.Products.AsNoTracking().Where(x => x.BrandId == id).ToList();
        }

        public void Save(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
        }

        public void Update(Brand brand)
        {
            _context.Entry<Brand>(brand).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Brand brand)
        {
            _context.Brands.Remove(brand);
            _context.SaveChanges();
        }
    }
}