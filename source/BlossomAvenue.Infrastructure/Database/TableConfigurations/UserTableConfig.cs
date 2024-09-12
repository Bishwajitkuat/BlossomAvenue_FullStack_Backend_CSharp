using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Core.ValueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlossomAvenue.Infrastructure.Database.TableConfigurations
{
    public class UserTableConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {

            b.Property<Guid>("UserId")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("user_id")
                .HasDefaultValueSql("gen_random_uuid()");

            b.Property<DateTime?>("CreatedAt")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            b.Property<string>("Email")
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .HasColumnName("email");

            b.Property<string>("FirstName")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasColumnName("first_name");

            b.Property<bool?>("IsUserActive")
                .HasColumnType("boolean")
                .HasColumnName("is_user_active");

            b.Property<DateTime?>("LastLogin")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("last_login");

            b.Property<string>("LastName")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasColumnName("last_name");

            b.Property(b => b.UserRole)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .HasColumnName("user_role")
                .HasConversion<string>();

            b.HasKey("UserId")
                .HasName("pk_users");

            b.ToTable("users", (string)null);

            b.HasData(SeedDataUsers.Users);
        }
    }
}