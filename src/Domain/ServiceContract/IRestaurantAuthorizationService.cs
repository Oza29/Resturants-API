using Domain.Constants;
using Domain.Entites;


namespace Domain.ServiceContract
{
    public interface IRestaurantAuthorizationService
    {
        bool Authorize(Restaurant restaurant, ResourceOperation operation);
    }
}