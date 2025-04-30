using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Exceptions;
using KasiCornerKota_Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;


namespace KasiCornerKota_Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting Restaurant with id: " + request.Id);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            await restaurantsRepository.Delete(restaurant);
        
        }
    }
}
