using AutoMapper;
using KasiCornerKota_Domain.Constants;
using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Exceptions;
using KasiCornerKota_Domain.Interfaces;
using KasiCornerKota_Domain.Repositories;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Moq;
using Xunit;


namespace KasiCornerKota_Application.Restaurants.Commands.UpdateRestaurant.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMoq;
        private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMoq;
        private readonly Mock<IMapper> _mapperMoq;
        private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthorizationServiceMoq;

        private readonly UpdateRestaurantCommandHandler _handler;

        public UpdateRestaurantCommandHandlerTests()
        {
            _loggerMoq = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
            _restaurantsRepositoryMoq = new Mock<IRestaurantsRepository>();
            _mapperMoq = new Mock<IMapper>();
            _restaurantAuthorizationServiceMoq = new Mock<IRestaurantAuthorizationService>();

            _handler = new UpdateRestaurantCommandHandler(_loggerMoq.Object, _restaurantsRepositoryMoq.Object,
                _mapperMoq.Object,
                _restaurantAuthorizationServiceMoq.Object);

        }
        [Fact()]
        public async Task Handle_ValidRequestHandler_ShouldUpdatedRestaurant()
        {
            //arrange
            var command = new UpdateRestaurantCommand
            {
                Id = 1,
                Name = "Updated Name",
                Description = "Updated Description",
                HasDelivery = true
            };
            var restaurant = new Restaurant
            {
                Id = 1,
                Name = "Old Name",
                Description = "Old Description",
                HasDelivery = true
            };

            _mapperMoq.Setup(m => m.Map(command, restaurant));

            _restaurantsRepositoryMoq
                .Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(restaurant);

            _restaurantAuthorizationServiceMoq
                .Setup(auth => auth.Authorize(It.IsAny<Restaurant>(), ResourceOperation.Update))
                .Returns(true);

            
            //act
            await _handler.Handle(command, CancellationToken.None);

            //assert
            _mapperMoq.Verify(m => m.Map(command, restaurant), Times.Once());
            _restaurantAuthorizationServiceMoq.Verify(v => v.Authorize(restaurant, ResourceOperation.Update), Times.Once());
            _restaurantsRepositoryMoq.Verify(r => r.SaveChanges(), Times.Once());
        }

        [Fact]
        public async Task Handle_RestaurantNotFound_ThrowsNotFoundException()
        {
            //arrange

            var command = new UpdateRestaurantCommand { Id = 1 };

            _restaurantsRepositoryMoq.Setup(repo => repo.GetByIdAsync(command.Id)).ReturnsAsync((Restaurant?)null);

            var handler = new UpdateRestaurantCommandHandler(
            _loggerMoq.Object,
            _restaurantsRepositoryMoq.Object,
            _mapperMoq.Object,
            _restaurantAuthorizationServiceMoq.Object);

            // Act & Assert
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Restaurant with id: {command.Id} does not exist");
        }
        [Fact]
        public async Task Handle_UserNotAuthorized_ThrowsForbidException()
        {
            // Arrange

            var command = new UpdateRestaurantCommand { Id = 2 };
            var restaurant = new Restaurant { Id = 2 };

            _restaurantsRepositoryMoq.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(restaurant);

            _restaurantAuthorizationServiceMoq.Setup(a => a.Authorize(restaurant, ResourceOperation.Update)).Returns(false);

            var handler = new UpdateRestaurantCommandHandler(
                _loggerMoq.Object,
                _restaurantsRepositoryMoq.Object,
                _mapperMoq.Object,
                _restaurantAuthorizationServiceMoq.Object);

            // Act & Assert
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ForbidException>();

            _restaurantsRepositoryMoq.Verify(r => r.SaveChanges(), Times.Never);
        }

    }
}