using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.ApplicationUser;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Services
{
    public class SeedService : ISeedService
    {
        private readonly UserSeeder _userSeeder;
        private readonly RolesSeeder _roleSeeder;
        private readonly ProductSeeder _productSeeder;
        private readonly CategorySeeder _categorySeeder;
        private readonly OrderSeeder _orderSeeder;

        private readonly ApplicationDbContext _context;

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedService(UserSeeder userSeeder, RolesSeeder roleSeeder,
                             ProductSeeder productSeeder, ApplicationDbContext context,
                             CategorySeeder categorySeeder, OrderSeeder orderSeeder,
                                UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userSeeder = userSeeder;
            _roleSeeder = roleSeeder;
            _productSeeder = productSeeder;
            _categorySeeder = categorySeeder;
            _orderSeeder = orderSeeder;

            _context = context;

            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task ExecuteSeedAsync()
        {
            await _roleSeeder.SeedAsync(_userManager, _roleManager);
            await _userSeeder.SeedAsync(_userManager, _roleManager);
            await _categorySeeder.SeedAsync(_context);
            await _productSeeder.SeedAsync(_context);
            await _orderSeeder.SeedAsync(_context);
            Console.WriteLine("---------------Seeding completed.-------------");
        }
    }
}