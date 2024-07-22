using Bigai.Restaurants.Application.Restaurants;

using Microsoft.AspNetCore.Mvc;

namespace Bigai.Restaurants.Api.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantsService _restaurantsService;

    public RestaurantsController(IRestaurantsService restaurantsService)
    {
        _restaurantsService = restaurantsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await _restaurantsService.GetAllAsync();

        return Ok(restaurants);
    }

}