using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.Entity<ApplicationUser>().ToTable("users");
            modelBuilder.Entity<ApplicationRole>().ToTable("roles");
            modelBuilder.Entity<IdentityUserRole<string>>(b => b.ToTable("user_roles"));
            modelBuilder.Entity<IdentityUserClaim<string>>(b => b.ToTable("user_claims"));
            modelBuilder.Entity<IdentityUserLogin<string>>(b => b.ToTable("user_logins"));
            modelBuilder.Entity<IdentityRoleClaim<string>>(b => b.ToTable("role_claims"));
            modelBuilder.Entity<IdentityUserToken<string>>(b => b.ToTable("user_tokens"));
        }
    }
}