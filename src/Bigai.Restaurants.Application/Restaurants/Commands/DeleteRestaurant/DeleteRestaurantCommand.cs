using MediatR;

namespace Bigai.Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommand : IRequest<bool>
{
    public int Id { get; }

    public DeleteRestaurantCommand(int id)
    {
        Id = id;
    }

}