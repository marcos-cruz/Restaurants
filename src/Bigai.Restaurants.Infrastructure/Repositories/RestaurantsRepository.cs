using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Repositories;
using Bigai.Restaurants.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace Bigai.Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository : IRestaurantsRepository
    {
        private readonly RestaurantsDbContext _dbContext;

        public RestaurantsRepository(RestaurantsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(Restaurant restaurant)
        {
            _dbContext.Restaurants.Add(restaurant);
            await _dbContext.SaveChangesAsync();

            return restaurant.Id;
        }

        public async Task DeleteAsync(Restaurant restaurant)
        {
            _dbContext.Restaurants.Remove(restaurant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await _dbContext.Restaurants.ToListAsync();

            return restaurants;
        }

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = _dbContext.Restaurants.Where(r => searchPhraseLower == null ||
                                                              (r.Name.ToLower().Contains(searchPhraseLower) ||
                                                               r.Description.ToLower().Contains(searchPhraseLower)));

            var totalCount = await baseQuery.CountAsync();

            var restaurants = await baseQuery.Skip(pageSize * (pageNumber - 1))
                                             .Take(pageSize)
                                             .ToListAsync();

            return (restaurants, totalCount);
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await _dbContext.Restaurants.Include(r => r.Dishes)
                                                         .FirstOrDefaultAsync(r => r.Id == id);

            return restaurant;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}