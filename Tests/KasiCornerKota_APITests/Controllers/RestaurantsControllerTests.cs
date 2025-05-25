using FluentAssertions;
using KasiCornerKota_APITests;
using KasiCornerKota_Application.Restaurants.Dtos;
using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Repositories;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;


namespace KasiCornerKota_API.Controllers.Tests
{
    public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMoq = new();
        public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, CornyPolicyEvaluator>();
                    services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsRepository), _=> _restaurantsRepositoryMoq.Object));
                });
            });
        }
        [Fact()]
        public async Task GetAll_ForValidRequest_Returns200Ok()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/restaurants?pageCount=1&pageSize=10");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact()]
        public async Task GetAll_ForInvalidRequest_ReturnsBadRequest()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/restaurants");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact()]
        public async Task GetById_ForNonExisting_ShouldReturn404NotFound()
        {
            var id = 111;
            _restaurantsRepositoryMoq.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((Restaurant?)null);

            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/api/restaurants/{id}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        [Fact()]
        public async Task GetById_ForExistingId_ShouldReturn200()
        {
            var restaurant = new Restaurant
            {
                Id = 9,
                Name = "Test Restaurant",
                Description = "Test Description",
            };
            _restaurantsRepositoryMoq.Setup(r => r.GetByIdAsync(restaurant.Id))
                .ReturnsAsync(restaurant);
             
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/restaurants/{restaurant.Id}");
            var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDto>();
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            restaurantDto.Should().NotBeNull();
            restaurantDto.Name.Should().Be(restaurant.Name);
            restaurantDto.Description.Should().Be(restaurant.Description);
        }
    }
}