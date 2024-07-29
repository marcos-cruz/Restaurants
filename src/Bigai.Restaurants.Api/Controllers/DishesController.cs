using Bigai.Restaurants.Application.Dishes.Commands.CreateDish;

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


    }
}