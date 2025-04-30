using KasiCornerKota_Application.Restaurants.Dtos;
using MediatR;


namespace KasiCornerKota_Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDto>>
    {
        
    }
}
