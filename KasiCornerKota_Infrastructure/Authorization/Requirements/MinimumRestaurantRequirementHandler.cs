using KasiCornerKota_Application.Users;
using KasiCornerKota_Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;


namespace KasiCornerKota_Infrastructure.Authorization.Requirements
{
    internal class MinimumRestaurantRequirementHandler(IRestaurantsRepository restaurantsRepository,
        IUserContext userContext) : AuthorizationHandler<MinimumRestaurantRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext handlerContext, MinimumRestaurantRequirement request)
        {
            var currentUser =  userContext.GetCurrentUser();


            var restaurants = await restaurantsRepository.GetAllAsync();

            var restaurantCount = restaurants.Count(c => c.OwnerId == currentUser!.Id);

            if (restaurantCount >= request.MinimumRestaurant)
            {
                handlerContext.Succeed(request);
            }
            else
            {
                handlerContext.Fail();
            }
        }
    }
}
