using Bigai.Restaurants.Application.Restaurants.Dtos;
using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Repositories;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Restaurants.Services;

internal class RestaurantsService : IRestaurantsService
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly ILogger<RestaurantsService> _logger;

    public RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger)
    {
        _restaurantsRepository = restaurantsRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
    {
        _logger.LogInformation("Getting all restaurants");

        var restaurants = await _restaurantsRepository.GetAllAsync();

        var restaurantsDto = restaurants.Select(RestaurantDto.FromEntity);

        return restaurantsDto!;
    }

    public async Task<RestaurantDto?> GetByIdAsync(int id)
    {
        _logger.LogInformation($"Getting restaurant {id}");

        var restaurant = await _restaurantsRepository.GetByIdAsync(id);
        var restaurantDto = RestaurantDto.FromEntity(restaurant);

        return restaurantDto;
    }
}