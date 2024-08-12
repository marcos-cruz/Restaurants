
using AutoMapper;

using Bigai.Restaurants.Application.Users;
using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly ILogger<CreateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public CreateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository,
                                          ILogger<CreateRestaurantCommandHandler> logger,
                                          IMapper mapper,
                                          IUserContext userContext)
    {
        _restaurantsRepository = restaurantsRepository;
        _logger = logger;
        _mapper = mapper;
        _userContext = userContext;
    }

    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();

        _logger.LogInformation("{UserEmail} {UserId} is Creating a new restaurant {@Restaurant}", currentUser?.Email, currentUser?.UserId, request);

        var restaurant = _mapper.Map<Restaurant>(request);
        restaurant.OwnerId = currentUser.UserId;

        var restaurantId = await _restaurantsRepository.CreateAsync(restaurant);

        return restaurantId;
    }
}