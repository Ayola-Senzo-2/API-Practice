using MediatR;


namespace KasiCornerKota_Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommand(int id) : IRequest<bool>
    {
        public int Id { get; } = id;
    }
}
