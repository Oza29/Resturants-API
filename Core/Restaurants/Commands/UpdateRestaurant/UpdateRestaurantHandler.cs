using AutoMapper;
using Core.RepositoryContracts;
using Domain.Constants;
using Domain.Entites;
using Domain.Exceptions;
using Domain.ServiceContract;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantHandler : IRequestHandler<UpdateRestaurantCommand>
    {
        private readonly ILogger<UpdateRestaurantHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;
        public UpdateRestaurantHandler(ILogger<UpdateRestaurantHandler>logger, IMapper mapper,
            IRestaurantRepository restaurantRepository, IRestaurantAuthorizationService restaurantAuthorizationService)
        {
            _mapper = mapper;
            _logger = logger;
            _restaurantRepository = restaurantRepository;
            _restaurantAuthorizationService = restaurantAuthorizationService;
        }
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating Restaurant with id: {@RestaurantId} with properties {@RestaurantProperties}",request.ID,request);
            var restaurant = await _restaurantRepository.GetRestaurantAsync(request.ID);
            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.ID.ToString());
            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new ForbiddenException();
            _mapper.Map(request, restaurant);
            await _restaurantRepository.UpdateRestaurant(restaurant);
            await _restaurantRepository.SaveChanges();
        }
    }
}
