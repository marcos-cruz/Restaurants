using Bigai.Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Bigai.Restaurants.Application.Users.Commands.AssignUserRole;
using Bigai.Restaurants.Application.Users.Commands.UnAssignUserRole;
using Bigai.Restaurants.Domain.Constants;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigai.Restaurants.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("user")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateRestaurantCommand command)
    {
        var isUpdated = await _mediator.Send(command);
        if (isUpdated)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpPost("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignUserRole([FromBody] AssignUserRoleCommand command)
    {
        var isUpdated = await _mediator.Send(command);
        if (isUpdated)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnAssignUserRole([FromBody] UnAssignUserRoleCommand command)
    {
        var isUpdated = await _mediator.Send(command);
        if (isUpdated)
        {
            return NoContent();
        }

        return NotFound();
    }

}