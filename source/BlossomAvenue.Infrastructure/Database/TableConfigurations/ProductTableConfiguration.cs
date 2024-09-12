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
    public class ProductTableConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> b)
        {
            b.Property<Guid>("ProductId")
            .ValueGeneratedOnAdd()
            .HasColumnType("uuid")
            .HasColumnName("product_id");

            b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

            b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

            b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasMaxLength(250)
                        .HasColumnName("title");

            b.HasKey("ProductId")
                        .HasName("pk_products");

            b.ToTable("products", (string)null);
            b.HasData(SeedDataProducts.Products);
        }
    }
}