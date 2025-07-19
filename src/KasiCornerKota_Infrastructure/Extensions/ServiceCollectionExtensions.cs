using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Interfaces;
using KasiCornerKota_Domain.Repositories;
using KasiCornerKota_Infrastructure.Authorization;
using KasiCornerKota_Infrastructure.Authorization.Requirements;
using KasiCornerKota_Infrastructure.Configuration;
using KasiCornerKota_Infrastructure.Persistence;
using KasiCornerKota_Infrastructure.Repositories;
using KasiCornerKota_Infrastructure.Seeder;
using KasiCornerKota_Infrastructure.Services;
using KasiCornerKota_Infrastructure.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<KasiKotaDbContext>();
          
            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "South Africa", "Lesotho"))
                .AddPolicy(PolicyNames.AtLeast20, builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
                .AddPolicy(PolicyNames.AtLeast2, builder => builder.AddRequirements(new MinimumRestaurantRequirement(2)));

            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
            services.AddScoped<IAuthorizationHandler, MinimumRestaurantRequirementHandler>();

            services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
            services.AddScoped<IBlobStorageService, BlobStorageService>();
        }
    }
}
