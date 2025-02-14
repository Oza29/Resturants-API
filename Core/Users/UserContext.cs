using Microsoft.AspNetCore.Http;

using System.Data;
using System.Security.Claims;

namespace Core.Users
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
    public class UserContext:IUserContext
    {
       private readonly IHttpContextAccessor _HttpContextAccessor;
        public UserContext(IHttpContextAccessor httpContext)
        {
            _HttpContextAccessor= httpContext;
        }

        public CurrentUser? GetCurrentUser()
        {
            var User = _HttpContextAccessor?.HttpContext?.User;
            if (User == null)
            {
                throw new InvalidOperationException("User context is not presented");
            }
            if (User.Identity == null|| !User.Identity.IsAuthenticated)
            {
                return null;
            }
            var userId= User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var Email = User.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var Roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            return new CurrentUser(userId,Email, Roles);
            
        }
    }
}
