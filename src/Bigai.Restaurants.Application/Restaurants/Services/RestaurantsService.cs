using AutoMapper;

using Bigai.Restaurants.Application.Restaurants.Dtos;
using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Repositories;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Restaurants.Services;

internal class RestaurantsService : IRestaurantsService
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly ILogger<RestaurantsService> _logger;
    private readonly IMapper _mapper;

    public RestaurantsService(IRestaurantsRepository restaurantsRepository,
                              ILogger<RestaurantsService> logger,
                              IMapper mapper)
    {
        _restaurantsRepository = restaurantsRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<int> CreateAsync(CreateRestaurantDto createRestaurantDto)
    {
        _logger.LogInformation("Creating a new restaurant");

        var restaurant = _mapper.Map<Restaurant>(createRestaurantDto);

        var restaurantId = await _restaurantsRepository.CreateAsync(restaurant);

        return restaurantId;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
    {
        _logger.LogInformation("Getting all restaurants");

        var restaurants = await _restaurantsRepository.GetAllAsync();

        var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        return restaurantsDto!;
    }

    public async Task<RestaurantDto?> GetByIdAsync(int id)
    {
        _logger.LogInformation($"Getting restaurant {id}");

        var restaurant = await _restaurantsRepository.GetByIdAsync(id);
        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

        return restaurantDto;
    }
}