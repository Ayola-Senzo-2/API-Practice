using KasiCornerKota_Application.Common;
using KasiCornerKota_Application.Constants;
using KasiCornerKota_Application.Restaurants.Dtos;
using MediatR;


namespace KasiCornerKota_Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery : IRequest<PagedResult<RestaurantDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
