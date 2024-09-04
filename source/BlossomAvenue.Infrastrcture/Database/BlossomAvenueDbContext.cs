using System;
using System.Collections.Generic;
using BlossomAvenue.Core.Users;
// using BlossomAvenue.Core.Products;
using BlossomAvenue.Core.ProductReviews;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.Carts;
using Microsoft.EntityFrameworkCore;
using BlossomAvenue.Core.Products;

namespace BlossomAvenue.Infrastrcture.Database;

public partial class BlossomAvenueDbContext : DbContext
{
    public BlossomAvenueDbContext()
    {
    }

    public BlossomAvenueDbContext(DbContextOptions<BlossomAvenueDbContext> options)
        : base(options)
    {
    }

    // public virtual DbSet<AddressDetail> AddressDetails { get; set; }

    // public virtual DbSet<Cart> Carts { get; set; }

    // public virtual DbSet<CartItem> CartItems { get; set; }

    // public virtual DbSet<Category> Categories { get; set; }

    // public virtual DbSet<City> Cities { get; set; }

    // public virtual DbSet<Image> Images { get; set; }

    // public virtual DbSet<Order> Orders { get; set; }

    // public virtual DbSet<OrderItem> OrderItems { get; set; }

    // public virtual DbSet<Product> Products { get; set; }

    // public virtual DbSet<ProductReview> ProductReviews { get; set; }

    // public virtual DbSet<ProductCategory> ProductsCategories { get; set; }

    // public virtual DbSet<User> Users { get; set; }

    // public virtual DbSet<UserAddress> UserAddresses { get; set; }

    // public virtual DbSet<UserContactNumber> UserContactNumbers { get; set; }

    // public virtual DbSet<UserCredential> UserCredentials { get; set; }

    // public virtual DbSet<UserRole> UserRoles { get; set; }

    // public virtual DbSet<Variation> Variations { get; set; }

    public virtual DbSet<AddressDetail> AddressDetails { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserContactNumber> UserContactNumbers { get; set; }

    public virtual DbSet<UserCredential> UserCredentials { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    // product related tables
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Image> Images { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Variation> Variations { get; set; }
    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    // order related tables
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<Cart> Carts { get; set; }
    public virtual DbSet<CartItem> CartItems { get; set; }
    public virtual DbSet<ProductReview> ProductReviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_trgm");

        modelBuilder.Entity<AddressDetail>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("address_details_pkey");

            entity.ToTable("address_details");

            entity.Property(e => e.AddressId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("address_id");
            entity.Property(e => e.AddressLine1)
                .HasMaxLength(100)
                .HasColumnName("address_line_1");
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(100)
                .HasColumnName("address_line_2");
            entity.Property(e => e.CityId).HasColumnName("city_id");

            entity.HasOne(d => d.City).WithMany(p => p.AddressDetails)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("address_details_city_id_fkey");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("cities_pkey");

            entity.ToTable("cities");

            entity.Property(e => e.CityId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("city_id");
            entity.Property(e => e.CityName)
                .HasMaxLength(50)
                .HasColumnName("city_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.IsUserActive)
                .HasDefaultValue(true)
                .HasColumnName("is_user_active");
            entity.Property(e => e.LastLogin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_login");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("users_user_role_id_fkey");

            entity.HasOne(d => d.UserCredential).WithOne(p => p.User);
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.AddressId }).HasName("user_addresses_pkey");

            entity.ToTable("user_addresses");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.DefaultAddress)
                .HasDefaultValue(false)
                .HasColumnName("default_address");

            entity.HasOne(d => d.Address).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("user_addresses_address_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("user_addresses_user_id_fkey");
        });

        modelBuilder.Entity<UserContactNumber>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ContactNumber }).HasName("user_contact_number_pkey");

            entity.ToTable("user_contact_number");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(10)
                .HasColumnName("contact_number");

            entity.HasOne(d => d.User).WithMany(p => p.UserContactNumbers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("user_contact_number_user_id_fkey");
        });

        modelBuilder.Entity<UserCredential>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_credentials_pk");

            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");

            entity.HasOne(d => d.User).WithOne(p => p.UserCredential)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("user_credentials_user_id_fkey");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("user_roles_pkey");

            entity.ToTable("user_roles");

            entity.Property(e => e.UserRoleId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("user_role_id");
            entity.Property(e => e.UserRoleName)
                .HasMaxLength(20)
                .HasColumnName("user_role_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}