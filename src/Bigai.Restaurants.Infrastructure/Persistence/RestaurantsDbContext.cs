using System.Reflection;

using Bigai.Restaurants.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Bigai.Restaurants.Infrastructure.Persistence;

internal class RestaurantsDbContext : DbContext
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost; Database=RestaurantsDb; User Id=sa; Password=Pass@word123; TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
                    .OwnsOne(r => r.Address);

        modelBuilder.Entity<Restaurant>()
                    .HasMany(r => r.Dishes)
                    .WithOne()
                    .HasForeignKey(d => d.RestaurantId);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
