using KasiCornerKota_Domain.Constants;
using KasiCornerKota_Domain.Entities;
using KasiCornerKota_Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


namespace KasiCornerKota_Application.Users.Command.DeleteUserRole
{
    public class UnassignUserRoleCommandHandler(ILogger<UnassignUserRoleCommand> logger, 
        UserManager<User> userManager) : IRequestHandler<UnassignUserRoleCommand>
    {
        public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Removing role to the user with {@Request}", request);
            var user = await userManager.FindByIdAsync(request.UserId)
                ?? throw new NotFoundException(nameof(User), request.UserId);
            var userRoles = await userManager.GetRolesAsync(user)
                ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

            if (!userRoles.Contains(request.RoleName))
            {
                throw new NotFoundException(nameof(IdentityRole), request.RoleName);
            }

            var result = await userManager.RemoveFromRoleAsync(user, request.RoleName);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Failed to remove role '{request.RoleName}' from user.");
            }
        }
    }
}
