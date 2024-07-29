using MediatR;

namespace Bigai.Restaurants.Application.Dishes.Commands.DeleteDishes;

public class DeleteDishesForRestaurantCommand : IRequest
{
    public int RestaurantId { get; set; }

    public DeleteDishesForRestaurantCommand(int restaurantId)
    {
        RestaurantId = restaurantId;
    }
}