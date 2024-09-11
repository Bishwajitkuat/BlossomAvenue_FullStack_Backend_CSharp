using System;
using System.Collections.Generic;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Core.ProductReviews;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.Carts;
using Microsoft.EntityFrameworkCore;
using BlossomAvenue.Core.Products;

namespace BlossomAvenue.Infrastructure.Database;

public partial class BlossomAvenueDbContext : DbContext
{
    public BlossomAvenueDbContext()
    {
    }

    public BlossomAvenueDbContext(DbContextOptions<BlossomAvenueDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddressDetail> AddressDetails { get; set; }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserAddress> UserAddresses { get; set; }
    public virtual DbSet<UserContactNumber> UserContactNumbers { get; set; }
    public virtual DbSet<UserCredential> UserCredentials { get; set; }

    // product related tables
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Image> Images { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Variation> Variations { get; set; }
    public virtual DbSet<ProductCategory> ProductCategory { get; set; }

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

        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.AddressId }).HasName("user_addresses_pkey");
        });

        modelBuilder.Entity<UserContactNumber>(entity =>
        {
            entity.HasKey(c => c.ContactNumberId);
        });

        modelBuilder.Entity<UserCredential>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_credentials_pk");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("created_at");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(c => c.CartId);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(ci => ci.CartItemsId);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.OrderId);
            entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("created_at");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(oi => oi.OrderItemsId);
        });

        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.HasKey(pr => pr.ReviewId);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}