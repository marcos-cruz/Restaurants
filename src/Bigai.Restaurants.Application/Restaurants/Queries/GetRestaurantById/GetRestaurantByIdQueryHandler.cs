
using AutoMapper;

using Bigai.Restaurants.Application.Restaurants.Dtos;
using Bigai.Restaurants.Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly ILogger<GetRestaurantByIdQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetRestaurantByIdQueryHandler(IRestaurantsRepository restaurantsRepository,
                                         ILogger<GetRestaurantByIdQueryHandler> logger,
                                         IMapper mapper)
    {
        _restaurantsRepository = restaurantsRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting restaurant {RestaurantId}", request.Id);

        var restaurant = await _restaurantsRepository.GetByIdAsync(request.Id);
        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

        return restaurantDto;
    }
}