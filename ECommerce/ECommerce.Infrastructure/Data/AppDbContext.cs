
using ECommerce.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Product>(e =>
            {
                e.Property(p => p.Name).IsRequired().HasMaxLength(200);
                e.Property(p => p.Sku).IsRequired().HasMaxLength(50);
                e.Property(p => p.Price).HasColumnType("decimal(18,2)");
                e.HasIndex(p => p.Sku).IsUnique();

                e.HasOne(p => p.Category).WithMany(c => c.Products)
                                         .HasForeignKey(p => p.CategoryId)
                                         .OnDelete(DeleteBehavior.Restrict);

              
            });

            modelBuilder.Entity<Customer>(e =>
            {
                e.Property(c => c.FullName).IsRequired().HasMaxLength(200);
                e.Property(c => c.Email).IsRequired().HasMaxLength(200);
            });


            modelBuilder.Entity<OrderItem>(e =>
            {
                e.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)");


            });
        }

    }
}
