using Xunit;
using Moq;

using KasiCornerKota_Application.Users;
using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using FluentAssertions;

namespace KasiCornerKota_Infrastructure.Authorization.Requirements.Tests
{
    public class MinimumRestaurantRequirementHandlerTests
    {
        private readonly Mock<IRestaurantsRepository> restaurantsRepoMoq;
        private readonly Mock<IUserContext> userContextMoq;

        public MinimumRestaurantRequirementHandlerTests()
        {
            restaurantsRepoMoq = new Mock<IRestaurantsRepository>();
            userContextMoq = new Mock<IUserContext>();
        }

        [Fact()]
        public async Task HandleRequirementAsync_UserHasMinimumRequirement_ShouldPassTest()
        {
            var currentUser = new CurrentUser("1", "test@test.co.za", [], null, null);
            userContextMoq.Setup(m => m.GetCurrentUser()).Returns(currentUser);

            var restaurants = new List<Restaurant>()
            {
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = "2",
                }
            };

            restaurantsRepoMoq.Setup(re => re.GetAllAsync()).ReturnsAsync(restaurants);

            var requirement = new MinimumRestaurantRequirement(2);
            var commandHander = new MinimumRestaurantRequirementHandler(restaurantsRepoMoq.Object, userContextMoq.Object);
            var context = new AuthorizationHandlerContext([requirement], null, null);

            //act
            await commandHander.HandleAsync(context);

            //assert
            context.HasSucceeded.Should().BeTrue();
        }

        [Fact()]
        public async Task HandleRequirementAsync_UserHasNotMetMinimumRequirement_ShouldFailTest()
        {
            var currentUser = new CurrentUser("1", "test@test.co.za", [], null, null);
            userContextMoq.Setup(m => m.GetCurrentUser()).Returns(currentUser);

            var restaurants = new List<Restaurant>()
            {
                new()
                {
                    OwnerId = currentUser.Id,
                },
                new()
                {
                    OwnerId = "2",
                }
            };

            restaurantsRepoMoq.Setup(re => re.GetAllAsync()).ReturnsAsync(restaurants);

            var requirement = new MinimumRestaurantRequirement(2);
            var commandHander = new MinimumRestaurantRequirementHandler(restaurantsRepoMoq.Object, userContextMoq.Object);
            var context = new AuthorizationHandlerContext([requirement], null, null);

            //act
            await commandHander.HandleAsync(context);

            //assert
            context.HasFailed.Should().BeTrue();
        }
    }
}