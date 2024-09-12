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
    public class VariationTableConfig : IEntityTypeConfiguration<Variation>
    {
        public void Configure(EntityTypeBuilder<Variation> b)
        {
            b.Property<Guid>("VariationId")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("variation_id");

            b.Property<int>("Inventory")
                .HasColumnType("integer")
                .HasColumnName("inventory");

            b.Property<decimal>("Price")
                .HasColumnType("numeric")
                .HasColumnName("price");

            b.Property<Guid>("ProductId")
                .HasColumnType("uuid")
                .HasColumnName("product_id");

            b.Property<string>("VariationName")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(250)
                .HasColumnName("variation_name");

            b.HasKey("VariationId")
                .HasName("pk_variations");

            b.HasIndex("ProductId")
                .HasDatabaseName("ix_variations_product_id");

            b.ToTable("variations", (string)null);
            b.HasData(SeedDataProducts.Variations);
        }
    }
}