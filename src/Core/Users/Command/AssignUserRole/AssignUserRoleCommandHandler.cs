using Domain.Constants;
using Domain.Entites;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Users.Command.AssignUserRole
{
    public class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand>
    {
        private readonly ILogger<AssignUserRoleCommandHandler> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole>_roleManager;
        public AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger,
            UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger= logger;
            _userManager= userManager;
            _roleManager= roleManager;  
            
        }
        public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Assigning a role {role} for a user with email {email}",request.RoleName,request.UserEmail);
            var user= await _userManager.FindByEmailAsync(request.UserEmail);
            if (user == null)
                throw new NotFoundException(nameof(User),request.UserEmail);
            var role = await _roleManager.FindByNameAsync(request.RoleName);
            if(role==null)
            throw new NotFoundException(nameof(IdentityRole), request.UserEmail);

            await _userManager.AddToRoleAsync(user, role.Name);
        }
    }
}
