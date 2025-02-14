using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace InfraStructure.Authorization
{
    public class RestaurantUserClaimsPrincipalFactory:UserClaimsPrincipalFactory<User,IdentityRole>
    {
        private readonly UserManager<User>_userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOptions<IdentityOptions> _options;

        public RestaurantUserClaimsPrincipalFactory(UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions>options)
            :base(userManager,roleManager,options)
        {
            _userManager= userManager;
            _roleManager= roleManager;
            _options= options;
        }
        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var id = await GenerateClaimsAsync(user);
            if (user.Nationality != null)
            {
                id.AddClaim(new Claim(nameof(user.Nationality), user.Nationality));
            }
            if (user.DateOfBirth != null)
            {
                id.AddClaim(new Claim(nameof(user.DateOfBirth), user.DateOfBirth.ToString()!));
            }
            return new ClaimsPrincipal(id);
        }
    }
}
