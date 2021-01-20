using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.BrandsViewModels;

namespace ProductCatalog.Repositories
{
    public class BrandRepository
    {
        private readonly StoreDataContext _context;

        public BrandRepository(StoreDataContext context)
        {
            _context = context;
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