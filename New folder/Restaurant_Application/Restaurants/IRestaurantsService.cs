
using Restaurant_Domain.Entities;

namespace Restaurant_Application.Restaurants
{
    public interface IRestaurantsService
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurants();
        Task<Restaurant?> GetById(int id);
    }
}
