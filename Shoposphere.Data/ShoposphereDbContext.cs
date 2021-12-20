 using Microsoft.EntityFrameworkCore;
using Shoposphere.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoposphere.Data
{
    public class ShoposphereDbContext: DbContext
    {
        public ShoposphereDbContext(DbContextOptions<ShoposphereDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<OrderDetail>().HasKey(x => new { x.OrderID, x.ProductID });
            modelBuilder.Entity<ProductSupplier>().HasKey(x => new { x.SupplierId, x.ProductId });

            modelBuilder.Entity<Category>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Comment>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Order>().Property(x => x.IsActive).HasDefaultValue(true);
            // modelBuilder.Entity<OrderDetail>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Product>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Role>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Shipper>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Supplier>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<User>().Property(x => x.IsActive).HasDefaultValue(true);    
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSupplier> productSuppliers { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
