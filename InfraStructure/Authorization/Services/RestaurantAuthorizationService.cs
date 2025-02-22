using Core.users;
using Core.Users;
using Domain.Constants;
using Domain.Entites;
using Domain.ServiceContract;
using Microsoft.Extensions.Logging;

namespace InfraStructure.Authorization.Services
{
    public class RestaurantAuthorizationService : IRestaurantAuthorizationService
    {
        private readonly ILogger<RestaurantAuthorizationService> _logger;
        private readonly IUserContext _userContext;

        public RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger, IUserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }
                  
        public bool Authorize(Restaurant restaurant, ResourceOperation operation)
        {
            var userClaim = _userContext.GetCurrentUser();
            _logger.LogInformation("Authorizing {UserEmail} to operation {operation} for restaurant {restaurant}",
                userClaim.Email, operation, restaurant);

            if (operation == ResourceOperation.Update || operation == ResourceOperation.Create)
            {
                _logger.LogInformation("Reading/Creating operation - successful authorization ");
                return true;
            }
            if (operation == ResourceOperation.Delete && userClaim.IsInRole(UserRoles.Admin))
            {
                _logger.LogInformation("Admin user-delete Operation -successful authorization");
                return true;
            }
            if (operation == ResourceOperation.Update || operation == ResourceOperation.Delete &&
                userClaim.Id == restaurant.OwnerId)
            {
                _logger.LogInformation("Restaurant owner-delete/update operation-successful authorization ");
                return true;
            }
            return false;
        }

    }
}
