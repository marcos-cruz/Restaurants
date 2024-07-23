using Bigai.Restaurants.Application.Restaurants.Dtos;

using MediatR;

namespace Bigai.Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQuery : IRequest<RestaurantDto?>
{
    public int Id { get; }

    public GetRestaurantByIdQuery(int id)
    {
        Id = id;
    }
}