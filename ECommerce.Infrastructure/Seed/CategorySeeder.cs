using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Seed
{
    public static class CategorySeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.Categories.AnyAsync()) return;

            var categories = new List<Category>
            {
                new Category { Id = Guid.NewGuid().ToString(), Name = "Action" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "RPG" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Shooter" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Simulation" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Strategy" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Horror" },
                new Category { Id = Guid.NewGuid().ToString(), Name = "Casual" },
            };

            foreach (var category in categories)
            {
                var slug = GenerateSlug(category.Name);
                category.Slug = await GetUniqueSlug(context, slug);
            }

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        private static string GenerateSlug(string name)
        {
            return name.ToLower().Replace(" ", "-");
        }

        private static async Task<string> GetUniqueSlug(ApplicationDbContext context, string baseSlug)
        {
            var slug = baseSlug;
            int counter = 1;

            while (await context.Categories.AnyAsync(c => c.Slug == slug))
            {
                slug = $"{baseSlug}-{counter}";
                counter++;
            }

            return slug;
        }
    }
}
