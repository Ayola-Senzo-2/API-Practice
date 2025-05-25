using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Constants;

namespace KasiCornerKota_Domain.Interfaces
{
    public interface IRestaurantAuthorizationService
    {
        bool Authorize(Restaurant restaurant, ResourceOperation operation);
    }
}