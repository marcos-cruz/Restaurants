using Bigai.Restaurants.Domain.Entities;

namespace Bigai.Restaurants.Application.Restaurants
{
    public interface IRestaurantsService
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
    }
}