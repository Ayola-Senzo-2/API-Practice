using Microsoft.AspNetCore.Authorization;

namespace KasiCornerKota_Infrastructure.Authorization.Requirements
{
    public class MinimumAgeRequirement(int minimumAge) : IAuthorizationRequirement
    {
        public int MinimumAge { get; } = minimumAge;
    }
}
