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

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await _dbContext.Restaurants.ToListAsync();

            return restaurants;
        }
    }
}