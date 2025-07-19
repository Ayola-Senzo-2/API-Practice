using KasiCornerKota_Domain.Constants;
using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KasiCornerKota_Infrastructure.Seeder
{
    internal class RestaurantSeeder(KasiKotaDbContext context) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                await context.Database.MigrateAsync();
            }
            if (await context.Database.CanConnectAsync())
            {
                if (!context.Restaurants.Any())
                {
                    var restaurant = GetRestaurants();
                    context.Restaurants.AddRange(restaurant);
                    await context.SaveChangesAsync();
                }
                if (!context.Roles.Any())
                {
                    var roles = GetRoles();
                    context.Roles.AddRange(roles);
                    await context.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
                [
                    new (UserRoles.User)
                    {
                        NormalizedName = UserRoles.User.ToUpper()
                    },
                    new (UserRoles.Owner)
                    {
                        NormalizedName = UserRoles.Owner.ToUpper()
                    },
                    new (UserRoles.Admin) 
                    { 
                        NormalizedName = UserRoles.Admin.ToUpper() 
                    },
                ];
            return roles;
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            User owner = new User()
            {
                Email = "seed-user@test.com"
            };

            List<Restaurant> restaurants = [
                new()
                {
                    Owner = owner,
                    Name = "KFC",
                    Category = "Fast Food",
                    Description =
                        "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    dishes =
                    [
                        new ()
                        {
                            Name = "Nashville Hot Chicken",
                            Description = "Nashville Hot Chicken (10 pcs.)",
                            Price = 10.30M,
                        },

                        new ()
                        {
                            Name = "Chicken Nuggets",
                            Description = "Chicken Nuggets (5 pcs.)",
                            Price = 5.30M,
                        },
                    ],
                    Address = new ()
                    {
                        City = "London",
                        Street = "Cork St 5",
                        PostalCode = "WC2N 5DU"
                    }
                },
                new ()
                {
                    Owner = owner,
                    Name = "McDonald",
                    Category = "Fast Food",
                    Description =
                        "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                    ContactEmail = "contact@mcdonald.com",
                    HasDelivery = true,
                    Address = new Address()
                    {
                        City = "London",
                        Street = "Boots 193",
                        PostalCode = "W1F 8SR"
                    }
                }
            ];
            return restaurants;
        }
    }
}
