using MediatR;

namespace Core.Users.Command.UnAssignUserRole
{
    public class UnAssignUserRoleCommand:IRequest
    {
        public string UserEmail { get; set; }
        public string RoleToRemove {  get; set; }
    }
}
