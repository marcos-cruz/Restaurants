using AutoMapper;

using Bigai.Restaurants.Domain.Enums;
using Bigai.Restaurants.Domain.Exceptions;
using Bigai.Restaurants.Domain.Repositories;
using Bigai.Restaurants.Domain.Services;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, bool>
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

    public UpdateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository,
                                          ILogger<UpdateRestaurantCommandHandler> logger,
                                          IMapper mapper,
                                          IRestaurantAuthorizationService restaurantAuthorizationService)
    {
        _restaurantsRepository = restaurantsRepository;
        _logger = logger;
        _mapper = mapper;
        _restaurantAuthorizationService = restaurantAuthorizationService;
    }

    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating restaurant {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);

        var restaurant = await _restaurantsRepository.GetByIdAsync(request.Id);
        if (restaurant is null)
        {
            return false;
        }

        if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            throw new ForbidException();
        }

        _mapper.Map(request, restaurant);

        await _restaurantsRepository.SaveChangesAsync();

        return true;
    }
}