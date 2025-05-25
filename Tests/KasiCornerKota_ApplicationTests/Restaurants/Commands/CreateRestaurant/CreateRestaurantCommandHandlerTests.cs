using Xunit;
using Moq;
using KasiCornerKota_Domain.Repositories;
using KasiCornerKota_Application.Users;
using Microsoft.Extensions.Logging;
using AutoMapper;
using KasiCornerKota_Domain.Entities;
using FluentAssertions;
using KasiCornerKota_Domain.Constants;
using KasiCornerKota_Domain.Interfaces;

namespace KasiCornerKota_Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
        {
            //arrange
            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
            var mapperMock = new Mock<IMapper>();

            var command = new CreateRestaurantCommand();
            var restaurant = new Restaurant();

            mapperMock.Setup(c => c.Map<Restaurant>(command)).Returns(restaurant);
            var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
            restaurantsRepositoryMock
                .Setup(repo => repo.AddByAsync(It.IsAny<Restaurant>()))
                .ReturnsAsync(1);

            var userContextMock = new Mock<IUserContext>();
            var currentUser = new CurrentUser("Owner-id", "test@test.com", [],null,null);
            userContextMock.Setup(user => user.GetCurrentUser()).Returns(currentUser);

            var restaurantAuthorizationMock = new Mock<IRestaurantAuthorizationService>();
            restaurantAuthorizationMock
                .Setup(auth => auth.Authorize(It.IsAny<Restaurant>(), ResourceOperation.Create))
                .Returns(true);

            var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object,
                mapperMock.Object,
                restaurantsRepositoryMock.Object,
                userContextMock.Object, restaurantAuthorizationMock.Object);
            //act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            //assert
            result.Should().Be(1);
            restaurant.OwnerId.Should().Be("Owner-id");
            restaurantsRepositoryMock.Verify(r => r.AddByAsync(restaurant), Times.Once);
            restaurantAuthorizationMock.Verify(a => a.Authorize(restaurant, ResourceOperation.Create), Times.Once);

        }
    }
}