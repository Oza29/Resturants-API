using AutoMapper;
using Core.RepositoryContracts;
using Domain.Constants;
using Domain.Entites;
using Domain.Exceptions;
using Domain.ServiceContract;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;
        public DeleteRestaurantCommandHandler(IRestaurantRepository repository, ILogger<DeleteRestaurantCommandHandler> logger
            , IMapper mapper, IRestaurantAuthorizationService restaurantAuthorizationService)
        {
            _restaurantRepository = repository;
            _logger = logger;
            _mapper = mapper;
            _restaurantAuthorizationService= restaurantAuthorizationService;
        }
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting a Restaurant with id:{@ResturantID}",request.ID);
           var restaurant= await _restaurantRepository.GetRestaurantAsync(request.ID);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.ID.ToString());
            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
                throw new ForbiddenException();

           await _restaurantRepository.DeleteRestaurant(restaurant);
            
        }
    }

}