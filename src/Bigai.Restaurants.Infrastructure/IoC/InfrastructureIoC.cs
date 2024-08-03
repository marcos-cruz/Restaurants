using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Repositories;
using Bigai.Restaurants.Infrastructure.Persistence;
using Bigai.Restaurants.Infrastructure.Repositories;
using Bigai.Restaurants.Infrastructure.Seeders;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bigai.Restaurants.Infrastructure.IoC;

/// <summary>
/// <see cref="InfrastructureIoC"/> Provides support for infrastructure layer dependency injection.
/// </summary>
public static class InfrastructureIoC
{
    /// <summary>
    /// Registers the given context as a service in the IServiceCollection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");

        services.AddDbContext<RestaurantsDbContext>(options =>
            options.UseSqlServer(connectionString)
                   .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<RestaurantsDbContext>();

        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();

        return services;
    }
}