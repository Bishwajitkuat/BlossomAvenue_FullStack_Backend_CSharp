using System;
using System.Collections.Generic;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Core.ProductReviews;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.Carts;
using Microsoft.EntityFrameworkCore;
using BlossomAvenue.Core.Products;
using System.Reflection;
using BlossomAvenue.Infrastructure.Database.SeedData;
using BlossomAvenue.Core.Authentication;

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
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserAddress> UserAddresses { get; set; }
    public virtual DbSet<UserContactNumber> UserContactNumbers { get; set; }
    public virtual DbSet<UserCredential> UserCredentials { get; set; }
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

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

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<UserAddress>(b =>
        {
            b.HasKey(e => e.UserAddressId);
            b.HasData(SeedDataUsers.UserAddresses);
        });

        modelBuilder.Entity<Image>(b =>
        {
            b.HasKey(b => b.ImageId);
            b.HasData(SeedDataProducts.Images);
        });

        modelBuilder.Entity<ProductCategory>(b =>
        {
            b.HasKey(b => b.ProductCategoryId);
            b.HasData(SeedDataProducts.ProductCategories);
        });

        modelBuilder.Entity<Cart>(b =>
        {
            b.HasKey(c => c.CartId);
            b.HasData(SeedDataUsers.Carts);
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

        modelBuilder.Entity<RefreshToken>(b =>
        {
            b.HasKey(b => b.RefreshTokenId);
            b.Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("created_at");
            b.Property(e => e.ExpiredAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("expired_at");
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