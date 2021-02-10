using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.ViewModels.CategoryViewModels;
using ProductCatalog_Domain;

namespace ProductCatalog.Repositories
{
    public class CategoryRepository
    {
        private readonly StoreDataContext _context;

        public CategoryRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<ListCategoryViewModel> GetAllCategories()
        {
            return _context.Categories
                    .Select(x => new ListCategoryViewModel
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                    .AsNoTracking()
                    .ToList();
        }

        public Category GetCategoryId(int id)
        {
            return _context.Categories.Find(id);
        }

        public IEnumerable<Product> GetProductsCategory(int id)
        {
            return _context.Products.AsNoTracking().Where(x => x.CategoryId == id).ToList();
        }

        public void Save(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Entry<Category>(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}