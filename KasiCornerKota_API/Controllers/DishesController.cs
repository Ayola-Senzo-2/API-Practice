using KasiCornerKota_Application.Dishes.Commands.CreateDish;
using KasiCornerKota_Application.Dishes.Commands.DeleteDish;
using KasiCornerKota_Application.Dishes.Dtos;
using KasiCornerKota_Application.Dishes.Queries.GetDishByIdForRestaurant;
using KasiCornerKota_Application.Dishes.Queries.GetDishesForRestaurant;
using KasiCornerKota_Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KasiCornerKota_API.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantId}/dishes")]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute]int restaurantId,CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;

           var dishId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId }, null);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }
        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
            return Ok(dish);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDishesForRestaurant([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteDishByIdCommand(restaurantId));
            return NoContent();
        }
    }
}
