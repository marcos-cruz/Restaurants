
using Bigai.Restaurants.Domain.Enums;
using Bigai.Restaurants.Domain.Exceptions;
using Bigai.Restaurants.Domain.Repositories;
using Bigai.Restaurants.Domain.Services;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, bool>
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
    private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

    public DeleteRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository,
                                          ILogger<DeleteRestaurantCommandHandler> logger,
                                          IRestaurantAuthorizationService restaurantAuthorizationService)
    {
        _restaurantsRepository = restaurantsRepository;
        _logger = logger;
        _restaurantAuthorizationService = restaurantAuthorizationService;
    }

    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting restaurant {RestaurantId}", request.Id);

        var restaurant = await _restaurantsRepository.GetByIdAsync(request.Id);

        if (restaurant is null)
        {
            return false;
        }

        if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
        {
            throw new ForbidException();
        }

        await _restaurantsRepository.DeleteAsync(restaurant);

        return true;
    }
}
