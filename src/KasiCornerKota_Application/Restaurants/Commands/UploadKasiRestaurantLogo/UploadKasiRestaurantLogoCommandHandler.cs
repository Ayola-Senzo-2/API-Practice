using KasiCornerKota_Domain.Constants;
using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Exceptions;
using KasiCornerKota_Domain.Interfaces;
using KasiCornerKota_Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KasiCornerKota_Application.Restaurants.Commands.UploadKasiRestaurantLogo
{
    internal class UploadKasiRestaurantLogoCommandHandler(ILogger<UploadKasiRestaurantLogoCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository, IRestaurantAuthorizationService restaurantAuthorizationService, 
        IBlobStorageService blobStorageService
        ) : IRequestHandler<UploadKasiRestaurantLogoCommand>
    {
        public async Task Handle(UploadKasiRestaurantLogoCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Uploading restaurant logo for id: {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new ForbidException("Not Authorize to update logo");

            var logoUrl = await blobStorageService.UploadToBlobAsync(request.File, request.FileName);
            restaurant.LogoUrl = logoUrl;

            await restaurantsRepository.SaveChanges();
        }
    }
}
