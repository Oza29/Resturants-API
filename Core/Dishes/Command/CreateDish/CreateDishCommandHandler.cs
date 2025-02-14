using AutoMapper;
using Core.RepositoryContracts;
using Domain.Constants;
using Domain.Entites;
using Domain.Exceptions;
using Domain.ServiceContract;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Dishes.Command.CreateDish
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, int>
    {
        private readonly ILogger<CreateDishCommandHandler> _logger;
        private readonly IDishRepository _dishRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;
        private readonly IMapper _mapper;
        public CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
            IDishRepository dishRepository, IRestaurantRepository restaurantRepository, IMapper mapper,
            IRestaurantAuthorizationService restaurantAuthorizationService)
        {
            _logger = logger;
            _mapper=mapper;
            _restaurantRepository= restaurantRepository;
            _dishRepository=dishRepository;
            _restaurantAuthorizationService=restaurantAuthorizationService;

        }
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new dish: {@DishRequest}", request);
          var restaurant=  await _restaurantRepository.GetRestaurantAsync(request.RestaurantID);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant),request.RestaurantID.ToString());
            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new ForbiddenException();
            Dish dish=  _mapper.Map<Dish>(request);
          int DishID =  await _dishRepository.Create(dish);
            return DishID;
        }
    }
}
