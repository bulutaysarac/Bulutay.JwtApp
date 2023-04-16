using Microsoft.EntityFrameworkCore;
using Onion.JwtApp.Domain.Entities;
using Onion.JwtApp.Persistance.Configurations;

namespace Onion.JwtApp.Persistance.Context
{
    public class JwtContext : DbContext
    {
        public DbSet<AppRole> AppRoles => Set<AppRole>();
        public DbSet<AppUser> AppUsers => Set<AppUser>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();

        public JwtContext(DbContextOptions<JwtContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
