using KasiCornerKota_Application.Dishes.Dtos;
using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Exceptions;
using KasiCornerKota_Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;


namespace KasiCornerKota_Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishByIdCommandHandler(ILogger<DeleteDishByIdCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,IDishesRepository dishesRepository) : IRequestHandler<DeleteDishByIdCommand>
    {
        public async Task Handle(DeleteDishByIdCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting dishes from restaurant with id: {RestaurantId}",
                request.RestaurantId);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            
            await dishesRepository.Delete(restaurant.dishes);

        }
    }
}
