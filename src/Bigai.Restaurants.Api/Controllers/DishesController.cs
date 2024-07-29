using Bigai.Restaurants.Application.Dishes.Commands.CreateDish;
using Bigai.Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Bigai.Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Bigai.Restaurants.Application.Restaurants.Dtos;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Bigai.Restaurants.Api.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantId}/dishes")]
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

            await _mediator.Send(command);

            return Created();
        }

        [HttpGet]
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



    }
}