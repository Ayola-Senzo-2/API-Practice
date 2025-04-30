

using AutoMapper;
using KasiCornerKota_Application.Dishes.Dtos;
using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Exceptions;
using KasiCornerKota_Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KasiCornerKota_Application.Dishes.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler> logger,
        IRestaurantsRepository restaurantsRepository, IMapper mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Fetching dish: {DishId}, for restaurant with id: {RestaurantId}",request.DishId,
                request.RestaurantId);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            
            var dish = restaurant.dishes.FirstOrDefault(d => d.Id == request.DishId);
            if (dish == null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            var result = mapper.Map<DishDto>(dish);
            return result;
        }
    }
}
