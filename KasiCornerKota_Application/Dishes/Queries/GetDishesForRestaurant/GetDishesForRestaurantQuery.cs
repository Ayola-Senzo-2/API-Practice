using KasiCornerKota_Application.Dishes.Dtos;
using MediatR;


namespace KasiCornerKota_Application.Dishes.Queries.GetDishesForRestaurant
{
    public class GetDishesForRestaurantQuery(int restaurantId) :IRequest<IEnumerable<DishDto>>
    {
        public int RestaurantId { get; set; } = restaurantId;
    }
}
