using KasiCornerKota_Application.Restaurants;
using KasiCornerKota_Application.Restaurants.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace KasiCornerKota_API.Controllers;
[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await restaurantsService.GetAllRestaurants();
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var restaurant = await restaurantsService.GetById(id);
        if (restaurant is null)
            return NotFound($"Restaurant with ID {id} not found");

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantDto dto)
    {
        int id = await restaurantsService.AddNewRestaurant(dto);
        return CreatedAtAction(nameof(GetById),new { id}, null);
    }
}
