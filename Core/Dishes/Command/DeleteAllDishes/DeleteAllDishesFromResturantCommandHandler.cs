using Core.RepositoryContracts;
using Domain.Constants;
using Domain.Entites;
using Domain.Exceptions;
using Domain.ServiceContract;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Core.Dishes.Command.DeleteAllDishes
{
    public class DeleteAllDishesFromResturantCommandHandler : IRequestHandler<DeleteAllDishesFromResturantCommand>
    {
        private readonly ILogger<DeleteAllDishesFromResturantCommandHandler> _logger;
        private readonly IDishRepository _dishRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;
        public DeleteAllDishesFromResturantCommandHandler(ILogger<DeleteAllDishesFromResturantCommandHandler> logger,
            IDishRepository dishRepository, IRestaurantRepository restaurantRepository,
            IRestaurantAuthorizationService  restaurantAuthorizationService )
        {
            _logger = logger;
            _restaurantRepository = restaurantRepository;
            _dishRepository = dishRepository;
            _restaurantAuthorizationService= restaurantAuthorizationService;

        }
        public async Task Handle(DeleteAllDishesFromResturantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Removing all dishes related to restaurant {restaurantId}", request.RestaurantId);

            var restaurant = await _restaurantRepository.GetRestaurantAsync(request.RestaurantId);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant),request.RestaurantId.ToString());
            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new ForbiddenException();
            await _dishRepository.Delete(restaurant.Dishes.ToList());
        }
    }
}
