using KasiCornerKota_Application.Restaurants;
using KasiCornerKota_Application.Restaurants.Commands.CreateRestaurant;
using KasiCornerKota_Application.Restaurants.Dtos;
using KasiCornerKota_Application.Restaurants.Queries.GetAllRestaurants;
using KasiCornerKota_Application.Restaurants.Queries.GetRestaurantById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KasiCornerKota_API.Controllers;
[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
        if (restaurant is null)
            return NotFound($"Restaurant with ID {id} not found");

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById),new { id}, null);
    }
}
