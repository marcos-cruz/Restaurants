using MediatR;

namespace Bigai.Restaurants.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommand : IRequest<bool>
{
    public string UserEmail { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}