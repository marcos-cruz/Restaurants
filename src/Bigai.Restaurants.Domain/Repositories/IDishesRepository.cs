using Bigai.Restaurants.Domain.Entities;

namespace Bigai.Restaurants.Domain.Repositories;

public interface IDishesRepository
{
    Task<int> CreateAsync(Dish dish);
}