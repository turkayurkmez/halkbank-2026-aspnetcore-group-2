using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            await db.Database.MigrateAsync();

            if (!await db.Categories.AnyAsync())
            {
                var electronics = new Category { Name = "Elektronik" };
                var clothing = new Category { Name = "Giyim" };
                var books = new Category { Name = "Kitap" };

                db.Categories.AddRange(electronics, clothing, books);
                 await db.SaveChangesAsync();

                db.Products.AddRange(
                      new Product { Name = "Kulaklık", CategoryId = electronics.Id, Price = 1000, Sku = "ELK-001", Stock = 100 },
                      new Product { Name = "Gömlek", CategoryId = clothing.Id, Price = 750, Sku = "GYM-001", Stock = 100 },
                      new Product { Name = "ASP.NET Core", CategoryId = books.Id, Price = 1000, Sku = "KTP-001", Stock = 100 }
                    );
            }

            if (!await db.Customers.AnyAsync())
            {
                db.Customers.AddRange(
                      new Customer { Email = "user1@example.com", FullName = "Filiz Aydın" },
                      new Customer { Email = "user2@example.com", FullName = "Sinan Altın" }
                    );
            }

            await db.SaveChangesAsync();
        }
    }
}
