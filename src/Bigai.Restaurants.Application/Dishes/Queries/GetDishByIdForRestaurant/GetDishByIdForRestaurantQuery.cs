using Bigai.Restaurants.Application.Restaurants.Dtos;

using MediatR;

namespace Bigai.Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQuery : IRequest<DishDto>
{
    public int RestaurantId { get; set; }
    public int DishId { get; set; }

    public GetDishByIdForRestaurantQuery(int restaurantId, int dishId)
    {
        RestaurantId = restaurantId;
        DishId = dishId;
    }
}