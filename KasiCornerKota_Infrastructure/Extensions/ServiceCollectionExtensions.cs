using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Repositories;
using KasiCornerKota_Infrastructure.Persistence;
using KasiCornerKota_Infrastructure.Repositories;
using KasiCornerKota_Infrastructure.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KasiCornerKota_Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("KasiCornerKota");
            services.AddDbContext<KasiKotaDbContext>(options => options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());

            services.AddIdentityApiEndpoints<User>()
                .AddEntityFrameworkStores<KasiKotaDbContext>();
          
            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
        }
    }
}
