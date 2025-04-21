using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Repositories;
using KasiCornerKota_Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace KasiCornerKota_Infrastructure.Repositories
{
    internal class RestaurantsRepository(KasiKotaDbContext KotaDbContext) : IRestaurantsRepository
    {
        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await KotaDbContext.Restaurants.ToListAsync();

            return restaurants;
        }
        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await KotaDbContext.Restaurants
                .Include(r => r.dishes)
                .FirstOrDefaultAsync(r => r.Id == id);
            return restaurant;
        }
        public async Task<int> AddByAsync(Restaurant entity)
        {
            KotaDbContext.Restaurants.Add(entity);
            await KotaDbContext.SaveChangesAsync();
            return entity.Id;
        }
    }
}
