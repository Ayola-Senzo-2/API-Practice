using AutoMapper;
using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Exceptions;
using KasiCornerKota_Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KasiCornerKota_Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,IMapper mapper) : IRequestHandler<UpdateRestaurantCommand>
    {
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Updating a Restaurant with id: {request.Id}");
            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            /*restaurant.Description = request.Name;
            restaurant.Description = request.Description;
            restaurant.HasDelivery = request.HasDelivery;*/
            mapper.Map(request, restaurant);

            await restaurantsRepository.SaveChanges();
        }
    }
}
