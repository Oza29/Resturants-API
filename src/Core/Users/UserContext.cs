using Core.Users;
using Domain.Entites;
using Microsoft.AspNetCore.Http;

using System.Data;
using System.Security.Claims;

namespace Core.users
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
            var user = _HttpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("user context is not presented");
            }
            if (user.Identity == null|| !user.Identity.IsAuthenticated)
            {
                return null;
            }
            var userId= user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            var nationality = user.FindFirst(c => c.Type == nameof(User.Nationality))?.Value;
            var dateOfBirthString = user.FindFirst(c => c.Type == nameof(User.DateOfBirth))?.Value;
            var dateOfBirth= dateOfBirthString==null? (DateOnly?)null:DateOnly.ParseExact(dateOfBirthString,"yyyy-MM-dd");

            return new CurrentUser(userId,email, roles,nationality,dateOfBirth);
            
        }
    }
}
