

using MediatR;

namespace KasiCornerKota_Application.Restaurants.Commands.UploadKasiRestaurantLogo
{
    public class UploadKasiRestaurantLogoCommand : IRequest
    {
        public int RestaurantId { get; set; }
        public string FileName { get; set; } = default!;
        public Stream File { get; set; } = default!;
    }
}
