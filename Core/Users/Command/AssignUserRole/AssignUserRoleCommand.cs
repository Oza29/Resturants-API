using MediatR;

namespace Core.Users.Command.AssignUserRole
{
    public class AssignUserRoleCommand:IRequest
    {
        public string UserEmail {  get; set; }
        public string RoleName {  get; set; }
    }
}
