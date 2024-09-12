using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Infrastructure.Database.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlossomAvenue.Infrastructure.Database.TableConfigurations
{
    public class CategoryTableConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.HasKey(c => c.CategoryId);
            builder.Property(c => c.CategoryName)
                            .HasColumnType("varchar")
                            .HasMaxLength(50)
                            .HasColumnName("category_name")
                            .IsRequired();
            builder.HasData(SeedDataCategories.Categories);

        }

    }
}