using AutoMapper;
using KasiCornerKota_Application.Restaurants.Dtos;
using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;


namespace KasiCornerKota_Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
        IMapper _mapper, IRestaurantsRepository _restaurantsRepository) :IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new Restaurant");
            var restaurant = _mapper.Map<Restaurant>(request);
            var id = await _restaurantsRepository.AddByAsync(restaurant);
            return id;
        }
    }
    
}
