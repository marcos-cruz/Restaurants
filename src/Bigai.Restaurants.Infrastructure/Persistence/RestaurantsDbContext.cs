using System.Reflection;

using Bigai.Restaurants.Domain.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bigai.Restaurants.Infrastructure.Persistence;

internal class RestaurantsDbContext : IdentityDbContext<User>
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }

    public RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : base(options)
    {

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

        modelBuilder.Entity<User>()
                    .HasMany(user => user.OwnerRestaurants)
                    .WithOne(restaurant => restaurant.Owner)
                    .HasForeignKey(restaurant => restaurant.OwnerId);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
