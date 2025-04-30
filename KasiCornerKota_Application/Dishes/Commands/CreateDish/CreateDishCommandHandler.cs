

using AutoMapper;
using KasiCornerKota_Domain.Exceptions;
using KasiCornerKota_Domain.Repositories;
using KasiCornerKota_Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KasiCornerKota_Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,IDishesRepository dishesRepository, IMapper mapper) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new dish: {@DishRequest}", request);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

            if (restaurant == null) throw new NotFoundException(nameof(Restaurants), request.RestaurantId.ToString());

            var dish = mapper.Map<Dish>(request);

            return await dishesRepository.Create(dish);

        }
    }
}
