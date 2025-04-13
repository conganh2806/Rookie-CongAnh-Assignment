using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Seed
{
    public class ProductSeeder
    {
        public async Task SeedAsync(ApplicationDbContext context)
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
                    Slug = "god-of-war",
                    Description = "Action-packed adventure game featuring Kratos, a Greek god, and his son.",
                    Price = 59.99m,
                    Discount = 0.1f,
                    Quantity = 100,
                    Sold = 20,
                    ProductStatus = ProductStatus.Active,
                    ImageURL = "https://example.com/god-of-war.jpg",
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Elden Ring",
                    Slug = "elden-ring",
                    Description = "An action RPG game from the creators of Dark Souls.",
                    Price = 69.99m,
                    Discount = 0.05f,
                    Quantity = 50,
                    Sold = 30,
                    ProductStatus = ProductStatus.Active,
                    ImageURL = "https://example.com/elden-ring.jpg",
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Call of Duty: Modern Warfare",
                    Slug = "call-of-duty-modern-warfare",
                    Description = "First-person shooter game with multiplayer and single-player modes.",
                    Price = 49.99m,
                    Discount = 0.2f,
                    Quantity = 150,
                    Sold = 60,
                    ProductStatus = ProductStatus.Active,
                    ImageURL = "https://example.com/call-of-duty.jpg",
                    IsFeatured = false,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Cyberpunk 2077",
                    Slug = "cyberpunk-2077",
                    Description = "Open-world RPG set in a dystopian future where technology and crime rule.",
                    Price = 79.99m,
                    Discount = 0.15f,
                    Quantity = 120,
                    Sold = 80,
                    ProductStatus = ProductStatus.Active,
                    ImageURL = "https://example.com/cyberpunk-2077.jpg",
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "The Witcher 3",
                    Slug = "the-witcher-3",
                    Description = "Epic RPG featuring Geralt of Rivia in a story-driven, open-world fantasy.",
                    Price = 39.99m,
                    Discount = 0.3f,
                    Quantity = 200,
                    Sold = 50,
                    ProductStatus = ProductStatus.Active,
                    ImageURL = "https://example.com/the-witcher-3.jpg",
                    IsFeatured = false,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Minecraft",
                    Slug = "minecraft",
                    Description = "Sandbox game where players build and explore pixelated worlds.",
                    Price = 29.99m,
                    Discount = 0.05f,
                    Quantity = 300,
                    Sold = 150,
                    ProductStatus = ProductStatus.Active,
                    ImageURL = "https://example.com/minecraft.jpg",
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Fortnite",
                    Slug = "fortnite",
                    Description = "Battle royale game with survival and building elements.",
                    Price = 0m, // Free-to-play
                    Discount = 0f,
                    Quantity = 500,
                    Sold = 200,
                    ProductStatus = ProductStatus.Active,
                    ImageURL = "https://example.com/fortnite.jpg",
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Assassin's Creed Valhalla",
                    Slug = "assassins-creed-valhalla",
                    Description = "Action-adventure game set in the Viking age with open-world exploration.",
                    Price = 59.99m,
                    Discount = 0.1f,
                    Quantity = 90,
                    Sold = 25,
                    ProductStatus = ProductStatus.Active,
                    ImageURL = "https://example.com/assassins-creed-valhalla.jpg",
                    IsFeatured = false,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "FIFA 21",
                    Slug = "fifa-21",
                    Description = "Soccer simulation video game with online and offline play modes.",
                    Price = 49.99m,
                    Discount = 0.2f,
                    Quantity = 100,
                    Sold = 40,
                    ProductStatus = ProductStatus.Active,
                    ImageURL = "https://example.com/fifa-21.jpg",
                    IsFeatured = false,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Gran Turismo 7",
                    Slug = "gran-turismo-7",
                    Description = "Realistic driving simulator with a focus on cars and racing.",
                    Price = 69.99m,
                    Discount = 0.05f,
                    Quantity = 60,
                    Sold = 20,
                    ProductStatus = ProductStatus.Active,
                    ImageURL = "https://example.com/gran-turismo-7.jpg",
                    IsFeatured = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }
}
