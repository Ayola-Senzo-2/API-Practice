using AutoMapper;
using KasiCornerKota_Application.Dishes.Commands.CreateDish;
using KasiCornerKota_Domain.Entities;

namespace KasiCornerKota_Application.Dishes.Dtos
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<CreateDishCommand, Dish>();
            CreateMap<Dish, DishDto>();
        }
    }
}
