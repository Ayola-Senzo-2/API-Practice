using AutoMapper;
using KasiCornerKota_Domain.Entities;

namespace KasiCornerKota_Application.Dishes
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<Dish, DishDto>();
        }
    }
}
