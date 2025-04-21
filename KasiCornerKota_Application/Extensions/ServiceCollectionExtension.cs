using FluentValidation;
using FluentValidation.AspNetCore;
using KasiCornerKota_Application.Restaurants;
using Microsoft.Extensions.DependencyInjection;

namespace KasiCornerKota_Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var ApplicationAssembly = typeof(ServiceCollectionExtension).Assembly;
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ApplicationAssembly));
            services.AddAutoMapper(ApplicationAssembly);
            services.AddValidatorsFromAssembly(ApplicationAssembly)
                .AddFluentValidationAutoValidation();

        }
    }
}
