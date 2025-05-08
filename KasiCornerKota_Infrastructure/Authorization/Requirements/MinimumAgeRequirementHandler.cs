using KasiCornerKota_Application.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace KasiCornerKota_Infrastructure.Authorization.Requirements
{
    internal class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,
        IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement ageRequirement)
        {
            var currentUser = userContext.GetCurrentUser();
            if (currentUser == null)
            {
                logger.LogInformation("User context is null");
                context.Fail();
                return Task.CompletedTask;
            }

            logger.LogInformation("User: {Email}, date of birth {DateOfBirth} - Handling MinimumAgeRequirement",
                currentUser.Email, currentUser.DateOfBirth);

            if (currentUser.DateOfBirth == null)
            {
                logger.LogInformation("User date of birth is null");
                context.Fail();
                return Task.CompletedTask;
            }

            if (currentUser.DateOfBirth.Value.AddYears(ageRequirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
            { 
                logger.LogInformation("Authorization succeeded");
                context.Succeed(ageRequirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
