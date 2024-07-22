using Bigai.Restaurants.Domain.Entities;

namespace Bigai.Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
}