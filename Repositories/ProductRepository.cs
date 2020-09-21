using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Repositories
{
    public class ProductRepository
    {
        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<ListProductViewModel> GetAllProducts()
        {
           return _context.Products
                    .Include(x => x.Category)
                    .Select(x => new ListProductViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        Category = x.Category.Name,
                        CategoryId = x.Category.Id
                    })
                    .AsNoTracking()
                    .ToList();
        }

        public Product GetProductsId(int id)
        {
            // return _context.Products.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            return _context.Products.Find(id);
        }

        public void Save(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
         public void Update(Product product)
        {
            _context.Entry<Product>(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}