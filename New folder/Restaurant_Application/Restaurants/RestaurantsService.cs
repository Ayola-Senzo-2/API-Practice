using Microsoft.Extensions.Logging;
using Restaurant_Domain.Entities;
using Restaurant_Domain.Repositories;

namespace Restaurant_Application.Restaurants
{
    internal class RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger) : IRestaurantsService
    {
        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            logger.LogInformation("Getting all restaurants");
            var restaurants = await restaurantsRepository.GetAllAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetById(int id)
        {
            logger.LogInformation($"Id {id}");
            var restaurant = await restaurantsRepository.GetByIdAsync(id);
            return restaurant;
        }
    }
}
