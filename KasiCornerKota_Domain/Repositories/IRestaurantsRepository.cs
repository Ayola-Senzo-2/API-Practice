using KasiCornerKota_Domain.Entities;

namespace KasiCornerKota_Domain.Repositories
{
    public interface IRestaurantsRepository
    {
         Task<IEnumerable<Restaurant>> GetAllAsync();
         Task<Restaurant?> GetByIdAsync(int id);
        Task<int> AddByAsync(Restaurant entity);
        Task Delete(Restaurant entity);
        Task SaveChanges();
    }
}
