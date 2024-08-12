using AutoMapper;

using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Enums;
using Bigai.Restaurants.Domain.Exceptions;
using Bigai.Restaurants.Domain.Repositories;
using Bigai.Restaurants.Domain.Services;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, int>
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly IDishesRepository _dishesRepository;
    private readonly ILogger<CreateDishCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

    public CreateDishCommandHandler(IRestaurantsRepository restaurantsRepository,
                                    IDishesRepository dishesRepository,
                                    ILogger<CreateDishCommandHandler> logger,
                                    IMapper mapper,
                                    IRestaurantAuthorizationService restaurantAuthorizationService)
    {
        _restaurantsRepository = restaurantsRepository;
        _dishesRepository = dishesRepository;
        _logger = logger;
        _mapper = mapper;
        _restaurantAuthorizationService = restaurantAuthorizationService;
    }

    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new dish {@DishRequest}", request);

        var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant == null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Create))
        {
            throw new ForbidException();
        }

        var dish = _mapper.Map<Dish>(request);

        var dishId = await _dishesRepository.CreateAsync(dish);

        return dishId;
    }
}
