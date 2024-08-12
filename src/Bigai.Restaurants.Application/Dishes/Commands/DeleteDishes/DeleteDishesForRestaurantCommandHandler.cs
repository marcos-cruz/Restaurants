using AutoMapper;

using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Enums;
using Bigai.Restaurants.Domain.Exceptions;
using Bigai.Restaurants.Domain.Repositories;
using Bigai.Restaurants.Domain.Services;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Dishes.Commands.DeleteDishes;

public class DeleteDishesForRestaurantCommandHandler : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly IDishesRepository _dishesRepository;
    private readonly ILogger<DeleteDishesForRestaurantCommandHandler> _logger;
    private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

    public DeleteDishesForRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository,
                                                   IDishesRepository dishesRepository,
                                                   ILogger<DeleteDishesForRestaurantCommandHandler> logger,
                                                   IRestaurantAuthorizationService restaurantAuthorizationService)
    {
        _restaurantsRepository = restaurantsRepository;
        _dishesRepository = dishesRepository;
        _logger = logger;
        _restaurantAuthorizationService = restaurantAuthorizationService;
    }

    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Removing all dishes from restaurant: {RestaurantId}", request.RestaurantId);

        var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
        {
            throw new ForbidException();
        }


        await _dishesRepository.DeleteAsync(restaurant.Dishes);
    }
}