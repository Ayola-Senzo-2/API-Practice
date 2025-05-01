using KasiCornerKota_Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KasiCornerKota_Infrastructure.Persistence;

internal class KasiKotaDbContext(DbContextOptions<KasiKotaDbContext> options) 
    : IdentityDbContext<User>(options)
{
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Dish> Dishes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
            .OwnsOne(a => a.Address);

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.dishes)
            .WithOne()
            .HasForeignKey(d => d.RestaurantId);
    }
}
