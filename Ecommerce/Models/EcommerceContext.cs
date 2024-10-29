
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models
{
    public class EcommerceContext:DbContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set;}

        public DbSet <ProductSize> ProductSize { get; set; }

        public DbSet <ProductProductSize> ProductProductSize { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User to Products (Seller's Products)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Products)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User to Orders (Customer's Orders)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order to OrderDetails
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product to OrderDetails
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderDetails)
                .WithOne(od => od.Product)
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductProductSize>()
        .HasKey(ps => new { ps.ProductId, ps.ProductSizeId });

            modelBuilder.Entity<ProductProductSize>()
                .HasOne(ps => ps.Product)
                .WithMany(p => p.ProductProductSizes)
                .HasForeignKey(ps => ps.ProductId);

            modelBuilder.Entity<ProductProductSize>()
                .HasOne(ps => ps.ProductSize)
                .WithMany()
                .HasForeignKey(ps => ps.ProductSizeId);

            modelBuilder.Entity<OrderDetail>()
        .HasOne(od => od.Order)
        .WithMany(o => o.OrderDetails)
        .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);

        }
    }


    
}
