using Bigai.Restaurants.Application.Dishes.Commands.CreateDish;
using Bigai.Restaurants.Application.Dishes.Commands.DeleteDishes;
using Bigai.Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Bigai.Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Bigai.Restaurants.Application.Restaurants.Dtos;
using Bigai.Restaurants.Infrastructure.Authorization;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bigai.Restaurants.Api.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantId}/dishes")]
    [Authorize]
    public class DishesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, [FromBody] CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;

            var dishId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId }, null);
        }

        [HttpGet]
        [Authorize(Policy = PolicyNames.HasAtLeast20)]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await _mediator.Send(new GetDishesForRestaurantQuery(restaurantId));

            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dishes = await _mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));

            return Ok(dishes);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDishesForRestauran([FromRoute] int restaurantId)
        {
            await _mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));

            return NotFound();
        }



    }
}