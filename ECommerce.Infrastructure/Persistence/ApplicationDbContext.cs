using ECommerce.Application.Domain.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.ApplicationUser;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        //UseSnakeCaseNamingConvention not working in AddDbContext
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<IdentityRole>().ToTable("roles");
            modelBuilder.Entity<IdentityUserRole<string>>(b => b.ToTable("user_roles"));
            modelBuilder.Entity<IdentityUserClaim<string>>(b => b.ToTable("user_claims"));
            modelBuilder.Entity<IdentityUserLogin<string>>(b => b.ToTable("user_logins"));
            modelBuilder.Entity<IdentityRoleClaim<string>>(b => b.ToTable("role_claims"));
            modelBuilder.Entity<IdentityUserToken<string>>(b => b.ToTable("user_tokens"));
        }
    }
}