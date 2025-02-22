using MediatR;

namespace Core.Users.Command.UpdateUserDetails
{
    public class UpdateUserDetailsCommand : IRequest
    {

        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
    }
}
