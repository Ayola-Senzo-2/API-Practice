using Microsoft.AspNetCore.Identity;
using System;


namespace KasiCornerKota_Domain.Entities
{
    public class User : IdentityUser
    {
        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        public List<Restaurant> OwnedRestaurants { get; set; } = [];
    }
}
