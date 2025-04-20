using KasiCornerKota_Infrastructure.Persistence;
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
            var ConnectionString = configuration.GetConnectionString("KasiCornerKota");
            services.AddDbContext<KasiKotaDbContext>(options => options.UseSqlServer(ConnectionString));

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        }
    }
}
