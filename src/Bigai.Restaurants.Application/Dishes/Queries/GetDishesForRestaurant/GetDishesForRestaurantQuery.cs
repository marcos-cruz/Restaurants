using Bigai.Restaurants.Application.Restaurants.Dtos;

using MediatR;

namespace Bigai.Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQuery : IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; set; }

    public GetDishesForRestaurantQuery(int restaurantId)
    {
        RestaurantId = restaurantId;
    }
}