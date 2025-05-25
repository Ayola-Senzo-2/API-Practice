using Microsoft.AspNetCore.Authorization;


namespace KasiCornerKota_Infrastructure.Authorization.Requirements
{
    public class MinimumRestaurantRequirement(int minimumRestaurant) : IAuthorizationRequirement
    {
        public int MinimumRestaurant { get; } = minimumRestaurant;
    }
}
