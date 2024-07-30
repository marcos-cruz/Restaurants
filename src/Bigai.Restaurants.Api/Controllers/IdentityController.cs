using Bigai.Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

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

}