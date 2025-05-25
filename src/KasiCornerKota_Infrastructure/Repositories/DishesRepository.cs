

using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Repositories;
using KasiCornerKota_Infrastructure.Persistence;

namespace KasiCornerKota_Infrastructure.Repositories
{
    internal class DishesRepository(KasiKotaDbContext context) : IDishesRepository
    {
        public async Task<int> Create(Dish entity)
        {
            context.Dishes.Add(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }
        public async Task Delete(IEnumerable<Dish> entities)
        {
            context.Dishes.RemoveRange(entities);
            await context.SaveChangesAsync();
        }
    }
}
