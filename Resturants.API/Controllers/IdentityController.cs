using Core.Users.Command.AssignUserRole;
using Core.Users.Command.UnAssignUserRole;
using Core.Users.Command.UpdateUserDetails;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Resturants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPut ("user")]
        [Authorize(Roles = UserRoles.User)]
        public async Task <IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
        {
            
           await _mediator.Send(command);
            return Ok();
        }
        
        [Authorize(Roles =UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await _mediator.Send(command);
                return Ok();
        }

        [HttpDelete]
        [Authorize (Roles = UserRoles.Admin)]
        public async Task<IActionResult> UnAssignUserRole(UnAssignUserRoleCommand commanad)
        {
           await _mediator.Send(commanad);
            return Ok();
        }
    }
}
