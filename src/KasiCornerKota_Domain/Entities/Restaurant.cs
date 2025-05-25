
namespace KasiCornerKota_Domain.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }

        public string? PhoneNumber { get; set; }
        public string? ContactEmail { get; set; }
        public Address? Address { get; set; }
        public List<Dish>  dishes{ get; set; } = new List<Dish>();

        public User Owner { get; set; } = default!;
        public string OwnerId { get; set; } = default!;
    }
}
