using Domain.Entites;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Core.Users.Command.UnAssignUserRole
{
    public class UnAssignUserRoleCommandHandler : IRequestHandler<UnAssignUserRoleCommand>
    {
        private readonly ILogger<UnAssignUserRoleCommandHandler> _logger;
        private readonly UserManager<User>_userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UnAssignUserRoleCommandHandler(ILogger<UnAssignUserRoleCommandHandler> logger,
            UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager; 
            _roleManager = roleManager;
        }
        public async Task Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UnAssigning a role form user with emain {email}", request.UserEmail);
          var user= await  _userManager.FindByEmailAsync(request.UserEmail);
            if (user == null)
                throw new NotFoundException(nameof(User),request.UserEmail);
            var isInRole = await _userManager.IsInRoleAsync(user, request.RoleToRemove);
           if(isInRole == false)
                throw new NotFoundException(nameof(IdentityRole), request.RoleToRemove);
            
            await  _userManager.RemoveFromRoleAsync(user,request.RoleToRemove.ToUpper());
        }
    }
}
