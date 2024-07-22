using Bigai.Restaurants.Infrastructure.Persistence;

using Microsoft.Extensions.DependencyInjection;

namespace Bigai.Restaurants.Infrastructure.Data.Extensions;

/// <summary>
/// <see cref="ServiceCollectionExtensions"/> Provides support for infrastructure layer dependency injection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the given context as a service in the IServiceCollection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<RestaurantsDbContext>();

        return services;
    }
}