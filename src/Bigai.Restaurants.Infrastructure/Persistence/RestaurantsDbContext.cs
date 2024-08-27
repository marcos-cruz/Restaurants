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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Restaurant>()
                    .OwnsOne(r => r.Address);

        builder.Entity<Restaurant>()
                    .HasMany(r => r.Dishes)
                    .WithOne()
                    .HasForeignKey(d => d.RestaurantId);

        builder.Entity<User>()
                    .HasMany(user => user.OwnerRestaurants)
                    .WithOne(restaurant => restaurant.Owner)
                    .HasForeignKey(restaurant => restaurant.OwnerId);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
