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
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Select(x => new Product
                {
                    Id = x.Id,
                    Name = x.Name,
                    BrandId = x.BrandId,
                    Brand = x.Brand,
                    CategoryId = x.CategoryId,
                    Category = x.Category,
                    CreateDate = x.CreateDate,
                    LastUpdateDate = x.LastUpdateDate,
                    Description = x.Description,
                    Image = x.Image,
                    Price = x.Price,
                    Quantity = x.Quantity
                })
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products.Where(x => x.Id == id).AsNoTracking().FirstAsync();
        }

        public async Task DeleteProductAync(Guid id)
        {
            try
            {
                Product obj = await _context.Products.FindAsync(id);
                _context.Entry<Product>(obj).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }


        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                _context.Entry<Product>(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error has occurred");
            }
        }
    }
}
