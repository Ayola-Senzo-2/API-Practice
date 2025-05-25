using KasiCornerKota_Application.Users.Command.AssignUserRole;
using KasiCornerKota_Application.Users.Command.DeleteUserRole;
using KasiCornerKota_Application.Users.Command.UpdateUserDetails;
using KasiCornerKota_Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KasiCornerKota_API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        
        [HttpPatch("user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
        {
            // Assuming you have a mediator to send the command
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRoles(AssignUserRoleCommand command)
        {
            // Assuming you have a mediator to send the command
            await mediator.Send(command);
            return NoContent();
        }
        [HttpDelete("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UnassignUserRoles(UnassignUserRoleCommand command)
        {
            // Assuming you have a mediator to send the command
            await mediator.Send(command);
            return NoContent();
        }
    }
}
