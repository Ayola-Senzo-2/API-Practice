using KasiCornerKota_Application.Restaurants;
using Microsoft.Extensions.DependencyInjection;

namespace KasiCornerKota_Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {

            services.AddScoped<IRestaurantsService, RestaurantsService>();
            
        }
    }
}
