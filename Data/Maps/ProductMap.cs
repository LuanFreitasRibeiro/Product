using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Models;

namespace ProductCatalog.Data.Maps
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.Description).IsRequired().HasMaxLength(1024).HasColumnType("varchar(1024)");
            builder.Property(x => x.Image).IsRequired().HasMaxLength(1024).HasColumnType("varchar(1024)");
            builder.Property(x => x.LastUpdateDate).IsRequired();
            builder.Property(x => x.Price).IsRequired().HasColumnType("money");
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(120).HasColumnType("varchar(120)");
            // aqui está sendo realizado o relacionamento 1:N
            // uma Category (Categoria) com muitos Products (Produtos)
            builder.HasOne(x => x.Category).WithMany(x => x.Products);
            // builder.HasOne(x => x.Brand).WithMany(x => x.Products);
        }
    }
}