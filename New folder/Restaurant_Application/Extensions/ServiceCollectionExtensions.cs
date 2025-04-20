using Microsoft.Extensions.DependencyInjection;
using Restaurant_Application.Restaurants;

namespace Restaurant_Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            
            services.AddScoped<IRestaurantsService, RestaurantsService>();
        }
    }
}
