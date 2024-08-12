using AutoMapper;

using Bigai.Restaurants.Application.Common;
using Bigai.Restaurants.Application.Restaurants.Dtos;
using Bigai.Restaurants.Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, PageResult<RestaurantDto>>
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly ILogger<GetAllRestaurantsQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetAllRestaurantsQueryHandler(IRestaurantsRepository restaurantsRepository,
                                         ILogger<GetAllRestaurantsQueryHandler> logger,
                                         IMapper mapper)
    {
        _restaurantsRepository = restaurantsRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PageResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all restaurants");

        var (restaurants, totalCount) = await _restaurantsRepository.GetAllMatchingAsync(request.SearchPhrase, request.PageSize, request.PageNumber);

        var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        var result = new PageResult<RestaurantDto>(restaurantsDto, totalCount, request.PageSize, request.PageNumber);

        return result;
    }
}