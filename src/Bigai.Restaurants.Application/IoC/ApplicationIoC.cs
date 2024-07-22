using Bigai.Restaurants.Application.Restaurants.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Bigai.Restaurants.Application.Ioc;

/// <summary>
/// <see cref="ApplicationIoC"/> Provides support for application layer dependency injection.
/// </summary>
public static class ApplicationIoC
{
    /// <summary>
    /// Registers the given context as a service in the IServiceCollection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantsService, RestaurantsService>();

        services.AddAutoMapper(typeof(ApplicationIoC).Assembly);


        return services;
    }
}