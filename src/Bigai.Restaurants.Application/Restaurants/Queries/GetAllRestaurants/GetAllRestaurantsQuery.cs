using Bigai.Restaurants.Application.Restaurants.Dtos;

using MediatR;

namespace Bigai.Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDto>>
{
    public GetAllRestaurantsQuery()
    {

    }

}