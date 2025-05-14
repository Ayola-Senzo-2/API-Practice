using KasiCornerKota_Application.Constants;
using KasiCornerKota_Domain.Entities;

namespace KasiCornerKota_Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageCount, string? sortBy, SortDirection sortDirection);
        Task<Restaurant?> GetByIdAsync(int id);
        Task<int> AddByAsync(Restaurant entity);
        Task Delete(Restaurant entity);
        Task SaveChanges();
    }
}
