﻿using KasiCornerKota_Application.Users;
using KasiCornerKota_Domain.Constants;
using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace KasiCornerKota_Infrastructure.Services
{
    public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
        IUserContext userContext) : IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurant restaurant, ResourceOperation operation)
        {
            var user = userContext.GetCurrentUser();

            logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}",
                user.Email,
                operation,
                restaurant.Name);

            if (operation == ResourceOperation.Read || operation == ResourceOperation.Create)
            {

                logger.LogInformation("Create/read operation - successful authorization");
                return true;
            }

            if (operation == ResourceOperation.Delete && user.IsRole(UserRoles.Admin))
            {
                logger.LogInformation("Admin user, delete operation - successful authorization");
                return true;
            }
            if (operation == ResourceOperation.Delete || operation == ResourceOperation.Update && user.Id == restaurant.OwnerId)
            {
                logger.LogInformation("Restaurant Owner, successful authorization");
                return true;
            }
            return false;
        }
    }
}
