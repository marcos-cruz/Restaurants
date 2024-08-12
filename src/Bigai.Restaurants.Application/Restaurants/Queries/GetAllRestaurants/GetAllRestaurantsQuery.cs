using Bigai.Restaurants.Application.Common;
using Bigai.Restaurants.Application.Restaurants.Dtos;

using MediatR;

namespace Bigai.Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<PageResult<RestaurantDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public GetAllRestaurantsQuery()
    {
    }
}