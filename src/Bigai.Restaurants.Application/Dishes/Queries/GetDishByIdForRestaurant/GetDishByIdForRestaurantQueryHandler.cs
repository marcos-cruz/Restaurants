
using AutoMapper;

using Bigai.Restaurants.Application.Restaurants.Dtos;
using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Exceptions;
using Bigai.Restaurants.Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQueryHandler : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly ILogger<GetDishByIdForRestaurantQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetDishByIdForRestaurantQueryHandler(IRestaurantsRepository restaurantsRepository,
                                                ILogger<GetDishByIdForRestaurantQueryHandler> logger,
                                                IMapper mapper)
    {
        _restaurantsRepository = restaurantsRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving dish: {DishId}, for restaurant with id: {RestaurantId}", request.DishId, request.RestaurantId);

        var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
        if (dish is null)
        {
            throw new NotFoundException(nameof(Dish), request.DishId.ToString());
        }

        var result = _mapper.Map<DishDto>(dish);

        return result;
    }
}