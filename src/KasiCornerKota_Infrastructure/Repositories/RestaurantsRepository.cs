using KasiCornerKota_Application.Constants;
using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Repositories;
using KasiCornerKota_Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq.Expressions;


namespace KasiCornerKota_Infrastructure.Repositories
{
    internal class RestaurantsRepository(KasiKotaDbContext KotaDbContext) : IRestaurantsRepository
    {
        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await KotaDbContext.Restaurants.ToListAsync();

            return restaurants;
        }

        public async Task<(IEnumerable<Restaurant>,int)> GetAllMatchingAsync(string? searchPhrase, int pageNumber, int pageSize, string? sortBy, SortDirection sortDirection)
        {
            var query = KotaDbContext.Restaurants.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                var search = searchPhrase.ToLower();
                query = query.Where(r => r.Name.ToLower().Contains(search)
                                      || r.Description.ToLower().Contains(search));
            }

            var totalCount = await query.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    { nameof(Restaurant.Name), r => r.Name },
                    { nameof(Restaurant.Description), r => r.Description },
                    { nameof(Restaurant.Category), r => r.Category }
                };

                var selectedColumn = columnsSelector[sortBy];
                query = sortDirection == SortDirection.Ascending
                    ? query.OrderBy(selectedColumn)
                    : query.OrderByDescending(selectedColumn);
            }
            // Apply pagination
            var restaurants = await query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (restaurants, totalCount);
        }


        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await KotaDbContext.Restaurants
                .Include(r => r.dishes)
                .FirstOrDefaultAsync(r => r.Id == id);
            return restaurant;
        }
        public Task SaveChanges() =>
             KotaDbContext.SaveChangesAsync();
        
        public async Task<int> AddByAsync(Restaurant entity)
        {
            KotaDbContext.Restaurants.Add(entity);
            await KotaDbContext.SaveChangesAsync();
            return entity.Id;
        }
        public async Task Delete(Restaurant entity)
        {   
            KotaDbContext.Remove(entity);
            await KotaDbContext.SaveChangesAsync();
        }
    }
}
