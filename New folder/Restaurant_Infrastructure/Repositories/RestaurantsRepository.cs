using Microsoft.EntityFrameworkCore;
using Restaurant_Domain.Entities;
using Restaurant_Domain.Repositories;
using Restaurant_Infrastructure.Persistence;

namespace Restaurant_Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantDbContext dbContext) : IRestaurantsRepository
    {
        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await dbContext.Restaurants.ToListAsync();
            return restaurants;
        }
        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await dbContext.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
            return restaurant;
        }
    }
}
