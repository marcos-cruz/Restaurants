
using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Repositories;
using Bigai.Restaurants.Infrastructure.Persistence;

namespace Bigai.Restaurants.Infrastructure.Repositories
{
    internal class DishesRepository : IDishesRepository
    {
        private readonly RestaurantsDbContext _dbContext;

        public DishesRepository(RestaurantsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(Dish dish)
        {
            _dbContext.Dishes.Add(dish);
            await _dbContext.SaveChangesAsync();

            return dish.Id;
        }

        public async Task DeleteAsync(IEnumerable<Dish> dishes)
        {
            _dbContext.Dishes.RemoveRange(dishes);

            await _dbContext.SaveChangesAsync();
        }
    }
}