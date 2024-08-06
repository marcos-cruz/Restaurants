using MediatR;

namespace Bigai.Restaurants.Application.Users.Commands.UnAssignUserRole;

public class UnAssignUserRoleCommand : IRequest<bool>
{
    public string UserEmail { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}