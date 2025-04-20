using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant_Domain.Repositories;
using Restaurant_Infrastructure.Persistence;
using Restaurant_Infrastructure.Repositories;
using Restaurant_Infrastructure.Seeder;

namespace Restaurant_Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var ConnectionString = configuration.GetConnectionString("RestaurantsDb");
            services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(ConnectionString));
            
            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        }
    }
}
