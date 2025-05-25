using KasiCornerKota_Application.Dishes.Dtos;
using MediatR;

namespace KasiCornerKota_Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishByIdCommand(int restaurantId) : IRequest
    {
        public int RestaurantId { get; set; } = restaurantId;
    }
}
