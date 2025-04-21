using AutoMapper;
using KasiCornerKota_Application.Restaurants.Dtos;
using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Repositories;
using Microsoft.Extensions.Logging;


namespace KasiCornerKota_Application.Restaurants
{
    public interface IRestaurantsService
    {
        Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
        Task<RestaurantDto?> GetById(int id);
        Task<int> AddNewRestaurant(CreateRestaurantDto dto);
    }

    internal class RestaurantsService(IRestaurantsRepository restaurantsRepository,
        IMapper mapper, ILogger<RestaurantsService> logger) : IRestaurantsService
    {
        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            logger.LogInformation("Fetching all Restaurants");
            var restaurants = await restaurantsRepository.GetAllAsync();
            var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);


            return restaurantsDto!;
        }
        public async Task<RestaurantDto?> GetById(int id)
        {
            logger.LogInformation($"Fetching Restaurant with ID: {id}");
            var restaurant = await restaurantsRepository.GetByIdAsync(id);

            var restaurantDto = restaurant == null ? null : mapper.Map<RestaurantDto?>(restaurant);
            return restaurantDto;
        }
        public async Task<int> AddNewRestaurant(CreateRestaurantDto dto)
        {
            logger.LogInformation("Creating a new Restaurant");
            var restaurant = mapper.Map<Restaurant>(dto);
            int id = await restaurantsRepository.AddByAsync(restaurant);
            
            return id;
        }
    }
}
