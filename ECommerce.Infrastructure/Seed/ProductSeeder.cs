using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Seed
{
    public static class ProductSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.Products.AnyAsync()) return;

            // Ensure categories are seeded first
            var actionId = await context.Categories
                .Where(c => c.Name == "Action")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            var rpgId = await context.Categories
                .Where(c => c.Name == "RPG")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            var shooterId = await context.Categories
                .Where(c => c.Name == "Shooter")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (actionId == null || rpgId == null || shooterId == null)
            {
                throw new InvalidOperationException(
                    "Game categories must be seeded before seeding products.");
            }

            var products = new List<Product>
            {
                new Product
                {
                    Name = "God of War",
                    Price = 59.99m,
                    Slug = "god-of-war",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Elden Ring",
                    Price = 69.99m,
                    Slug = "elden-ring",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Call of Duty: Modern Warfare",
                    Price = 49.99m,
                    Slug = "call-of-duty-modern-warfare",
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }
}
