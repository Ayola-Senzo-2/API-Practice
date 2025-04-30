

using KasiCornerKota_Domain.Entities;

namespace KasiCornerKota_Domain.Repositories
{
    public interface IDishesRepository
    {
        Task<int> Create(Dish entity);
        Task Delete(IEnumerable<Dish> entities);
    }
}
