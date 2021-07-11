using Microsoft.EntityFrameworkCore;
using ProductCatagalog.Data.Maps;
using ProductCatalog.Data.Maps;
using ProductCatalog.Domain;

namespace ProductCatalog.Data
{
    public class StoreDataContext : DbContext
    {
        public StoreDataContext(DbContextOptions<StoreDataContext> options) : base(options) 
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new BrandMap());
            builder.ApplyConfiguration(new UserMap());
        }
    }
}
