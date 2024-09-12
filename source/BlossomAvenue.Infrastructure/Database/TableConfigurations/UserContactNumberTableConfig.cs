using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlossomAvenue.Infrastructure.Database.TableConfigurations
{
    public class UserContactNumberTableConfig : IEntityTypeConfiguration<UserContactNumber>
    {
        public void Configure(EntityTypeBuilder<UserContactNumber> b)
        {
            b.Property<Guid>("ContactNumberId")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("contact_number_id");

            b.Property<string>("ContactNumber")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(15)
                .HasColumnName("contact_number");

            b.Property<Guid>("UserId")
                .HasColumnType("uuid")
                .HasColumnName("user_id");

            b.HasKey("ContactNumberId")
                .HasName("pk_user_contact_numbers");

            b.HasIndex("UserId")
                .HasDatabaseName("ix_user_contact_numbers_user_id");

            b.ToTable("user_contact_numbers", (string)null);

            b.HasData(SeedDataUsers.UserContactNumbers);

        }
    }
}