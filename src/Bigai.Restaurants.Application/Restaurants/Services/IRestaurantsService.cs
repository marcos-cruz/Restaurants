using Bigai.Restaurants.Application.Restaurants.Dtos;

namespace Bigai.Restaurants.Application.Restaurants.Services;

public interface IRestaurantsService
{
    Task<IEnumerable<RestaurantDto>> GetAllAsync();
    Task<RestaurantDto?> GetByIdAsync(int id);
}