
using Bigai.Restaurants.Domain.Constants;
using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Infrastructure.Persistence;

using FluentValidation.TestHelper;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bigai.Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder : IRestaurantSeeder
{
    private readonly RestaurantsDbContext _dbContext;

    public RestaurantSeeder(RestaurantsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Seed()
    {
        if (_dbContext.Database.GetPendingMigrations().Any())
        {
            await _dbContext.Database.MigrateAsync();
        }

        if (await _dbContext.Database.CanConnectAsync())
        {
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                _dbContext.Restaurants.AddRange(restaurants);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        User owner = new User()
        {
            Email = "owner-seed@test.com",

        };

        List<Restaurant> restaurants = [
            new()
            {
                Owner = owner,
                Name = "KFC",
                Category = "Fast Food",
                Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new ()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },

                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                Address = new ()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                }
            },
            new ()
            {
                Owner = owner,
                Name = "McDonald",
                Category = "Fast Food",
                Description = "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                }
            }
        ];

        return restaurants;
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roleList = [
            new (UserRoles.Admin)
            {
                NormalizedName = UserRoles.Admin.ToUpper(),
            },
            new (UserRoles.Owner)
            {
                NormalizedName = UserRoles.Owner.ToUpper(),
            },
            new (UserRoles.User)
            {
                NormalizedName = UserRoles.User.ToUpper(),
            },
        ];

        return roleList;
    }
}