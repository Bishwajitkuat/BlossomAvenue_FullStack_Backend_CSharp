using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Core.ValueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlossomAvenue.Infrastructure.Database.TableConfigurations
{
    public class AddressDetailTableConfig : IEntityTypeConfiguration<AddressDetail>
    {
        public void Configure(EntityTypeBuilder<AddressDetail> b)
        {
            b.Property<Guid>("AddressDetailId")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("address_detail_id");

            b.Property<string>("AddressLine1")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(250)
                .HasColumnName("address_line1");

            b.Property<string>("AddressLine2")
                .HasColumnType("varchar")
                .HasMaxLength(250)
                .HasColumnName("address_line2");

            b.Property<string>("City")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasColumnName("city");

            b.Property(b => b.Country)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .HasColumnName("country")
                .HasConversion<string>();

            b.Property<string>("FullName")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(250)
                .HasColumnName("full_name");

            b.Property<string>("PostCode")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .HasColumnName("post_code");

            b.HasKey("AddressDetailId")
                .HasName("address_details_pkey");

            b.ToTable("address_details", (string)null);

            b.HasData(SeedDataUsers.AddressDetails);
        }
    }
}