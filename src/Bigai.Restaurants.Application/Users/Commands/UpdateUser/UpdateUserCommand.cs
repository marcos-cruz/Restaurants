using MediatR;

namespace Bigai.Restaurants.Application.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<bool>
{
    public DateOnly? DateOfBirth { get; set; }

    public string? Nationality { get; set; }
}