using Bigai.Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Bigai.Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Bigai.Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Bigai.Restaurants.Application.Restaurants.Dtos;
using Bigai.Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Bigai.Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Bigai.Restaurants.Domain.Constants;
using Bigai.Restaurants.Infrastructure.Authorization;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigai.Restaurants.Api.Controllers;

[ApiController]
[Route("api/restaurants")]
[Authorize]
public class RestaurantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestaurantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RestaurantDto>))]
    // public async Task<IActionResult> GetAll()
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
    {
        var restaurants = await _mediator.Send(new GetAllRestaurantsQuery());

        return Ok(restaurants);
    }

    [HttpGet]
    [Route("{id}")]
    [Authorize(Policy = PolicyNames.HasNationality)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)
    {
        var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));
        if (restaurant is null)
        {
            return NotFound();
        }

        return Ok(restaurant);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Owner)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var isDeleted = await _mediator.Send(new DeleteRestaurantCommand(id));
        if (isDeleted)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpPatch]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateRestaurantCommand command)
    {
        command.Id = id;

        var isUpdated = await _mediator.Send(command);
        if (isUpdated)
        {
            return NoContent();
        }

        return NotFound();
    }

}