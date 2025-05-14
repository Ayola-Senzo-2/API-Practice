

using AutoMapper;
using KasiCornerKota_Application.Common;
using KasiCornerKota_Application.Restaurants.Dtos;
using KasiCornerKota_Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace KasiCornerKota_Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger,
        IRestaurantsRepository restaurantsRepository, IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
    {
        public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            
            logger.LogInformation("Fetching all Restaurants");
            
            var (restaurants, totalCount) = await restaurantsRepository.GetAllMatchingAsync(
                request.SearchPhrase,
                request.PageCount,
                request.PageSize,
                request.SortBy,
                request.SortDirection
            );
            var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            var results = new PagedResult<RestaurantDto>(restaurantsDto, totalCount, request.PageSize, request.PageCount);

            return results!;
        }
    }
}
