
using AutoMapper;

using Bigai.Restaurants.Application.Restaurants.Dtos;
using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Exceptions;
using Bigai.Restaurants.Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQueryHandler : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly ILogger<GetDishesForRestaurantQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetDishesForRestaurantQueryHandler(IRestaurantsRepository restaurantsRepository,
                                              ILogger<GetDishesForRestaurantQueryHandler> logger,
                                              IMapper mapper)
    {
        _restaurantsRepository = restaurantsRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all dishes for restaurant with id: {RestaurantId}", request.RestaurantId);

        var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        var dishes = _mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);

        return dishes!;
    }
}