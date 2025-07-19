using KasiCornerKota_Application.Restaurants.Commands.CreateRestaurant;
using KasiCornerKota_Application.Restaurants.Commands.DeleteRestaurant;
using KasiCornerKota_Application.Restaurants.Commands.UpdateRestaurant;
using KasiCornerKota_Application.Restaurants.Commands.UploadKasiRestaurantLogo;
using KasiCornerKota_Application.Restaurants.Dtos;
using KasiCornerKota_Application.Restaurants.Queries.GetAllRestaurants;
using KasiCornerKota_Application.Restaurants.Queries.GetRestaurantById;
using KasiCornerKota_Domain.Constants;
using KasiCornerKota_Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KasiCornerKota_API.Controllers;
[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    /*[Authorize(Policy = PolicyNames.AtLeast2)]*/
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetAllRestaurantsQuery query)
    {
        var restaurants = await mediator.Send(query);
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    /*[Authorize(Policy = PolicyNames.HasNationality)]*/
    public async Task<ActionResult<RestaurantDto?>> GetById(int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
        return Ok(restaurant);
    }
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
    {
        command.Id = id;
         await mediator.Send(command);

            return NoContent();

    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRestaurant([FromRoute]int id)
    {
         await mediator.Send(new DeleteRestaurantCommand(id));
        
         return NoContent();
    }
    [HttpPost]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantCommand command)
    {

        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById),new { id}, null);
    }
    [HttpPost("{id}/logo")]
    public async Task<IActionResult> UploadLogo([FromRoute]int id, IFormFile file)
    {
        using var stream = file.OpenReadStream();

        var command = new UploadKasiRestaurantLogoCommand()
        {
            RestaurantId = id,
            FileName = $"{id}-{file.FileName}",
            File = stream
        };
        await mediator.Send(command);
        return NoContent();
    }
}
