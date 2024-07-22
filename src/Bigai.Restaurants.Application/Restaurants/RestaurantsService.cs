using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Repositories;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Restaurants;

internal class RestaurantsService : IRestaurantsService
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly ILogger<RestaurantsService> _logger;

    public RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger)
    {
        _restaurantsRepository = restaurantsRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        _logger.LogInformation("Getting all restaurants");

        var restaurants = await _restaurantsRepository.GetAllAsync();

        return restaurants;
    }
}