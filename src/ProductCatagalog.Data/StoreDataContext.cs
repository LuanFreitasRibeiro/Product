using Microsoft.EntityFrameworkCore;
using ProductCatagalog.Data.Maps;
using ProductCatalog.Data.Maps;
using ProductCatalog.Domain;

namespace ProductCatalog.Data
{
    public class StoreDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=sqlserver-prodcatalog;User ID=SA;Password=yourStrong(!)Password");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new BrandMap());
            builder.ApplyConfiguration(new UserMap());
        }
    }
}
