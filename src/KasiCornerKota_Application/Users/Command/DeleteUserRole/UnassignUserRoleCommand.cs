using MediatR;

namespace KasiCornerKota_Application.Users.Command.DeleteUserRole
{
    public class UnassignUserRoleCommand : IRequest
    {
        public string UserId { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}
