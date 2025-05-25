using AutoMapper;
using KasiCornerKota_Application.Restaurants.Dtos;
using KasiCornerKota_Application.Users;
using KasiCornerKota_Domain.Constants;
using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Exceptions;
using KasiCornerKota_Domain.Interfaces;
using KasiCornerKota_Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;


namespace KasiCornerKota_Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
        IMapper _mapper, IRestaurantsRepository _restaurantsRepository,IUserContext userContext, IRestaurantAuthorizationService restaurantAuthorization) :IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("{UserEmail} [{UserId}] is creating a new Restaurant {@Restaurant}",
                currentUser.Email,
                currentUser.Id,
                request);

            var restaurant = _mapper.Map<Restaurant>(request);
            restaurant.OwnerId = currentUser.Id;


            int id = await _restaurantsRepository.AddByAsync(restaurant);

            if (!restaurantAuthorization.Authorize(restaurant, ResourceOperation.Create))
            {
                throw new ForbidException("You are not authorized to delete this restaurant.");
            }

            return id;
        }
    }
    
}
