using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatagalog.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(20).HasColumnType("varchar(20)");
            builder.Property(x => x.Password).IsRequired().HasMaxLength(20).HasColumnType("varchar(20)");
            builder.Property(x => x.Role).IsRequired().HasMaxLength(15).HasColumnType("varchar(15)");
        }
    }
}
