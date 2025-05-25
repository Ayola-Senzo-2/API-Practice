using Xunit;
using AutoMapper;
using KasiCornerKota_Domain.Entities;
using FluentAssertions;
using KasiCornerKota_Application.Restaurants.Commands.CreateRestaurant;
using KasiCornerKota_Application.Restaurants.Commands.UpdateRestaurant;

namespace KasiCornerKota_Application.Restaurants.Dtos.Tests
{
    
    public class RestaurantProfileTests
    {

        private readonly IMapper _mapper;

        public RestaurantProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantProfile>();
            });

            
            _mapper = config.CreateMapper();
        }

        [Fact()]
        public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
        {
            //arrange

            var restaurant = new Restaurant()
            {
                Id = 1,
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@restaurant.com",
                PhoneNumber = "1234567890",
                Address = new Address
                {
                    City = "Mambisa",
                    Street = "Madiba Drive",
                    PostalCode = "1632"
                }
            };

            //act 
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            //assert
            restaurantDto.Should().NotBeNull();
            restaurantDto.Id.Should().Be(restaurant.Id);
            restaurantDto.Name.Should().Be(restaurant.Name);
            restaurantDto.Description.Should().Be(restaurant.Description);
            restaurantDto.Category.Should().Be(restaurant.Category);
            restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
            restaurantDto.City.Should().Be(restaurant.Address.City);
            restaurantDto.Street.Should().Be(restaurant.Address.Street);
            restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);

        }

        [Fact]
        public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapsCorrectly()
        {
            //arrange
            var command = new CreateRestaurantCommand()
            {
               
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@restaurant.com",
                PhoneNumber = "1234567890",
                City = "Mambisa",
                Street = "Madiba Drive",
                PostalCode = "1632"
                
            };

            //act 
            var restaurant = _mapper.Map<Restaurant>(command);

            //assert
            restaurant.Should().NotBeNull();
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.Category.Should().Be(command.Category);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);

            restaurant.Address.Should().NotBeNull();
            restaurant.Address.City.Should().Be(command.City);
            restaurant.Address.Street.Should().Be(command.Street);
            restaurant.Address.PostalCode.Should().Be(command.PostalCode);

        }
        [Fact]
        public void CreateMap_UpdateRestaurantCommandToRestaurant_MapsCorrectly()
        {
            //arrange
            var command = new UpdateRestaurantCommand()
            {
                Id = 1,
                Name = "Update Restaurant",
                Description = "Update Description",
                HasDelivery = false
            };

            var restaurant = _mapper.Map<Restaurant>(command);

            //assert
            restaurant.Should().NotBeNull();
            restaurant.Id.Should().Be(command.Id);
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);

        }
    }
}