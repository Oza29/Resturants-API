using AutoMapper;
using Core.RepositoryContracts;
using Core.Users;
using Domain.Entites;
using Domain.ServiceContract;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
    {
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserContext _userContext;
        public CreateRestaurantCommandHandler(IRestaurantRepository repository,ILogger<CreateRestaurantCommandHandler> logger
            ,IMapper mapper,IUserContext userContext)
        {
            _restaurantRepository = repository;
            _logger = logger;
            _mapper = mapper;
            _userContext = userContext;
            
        }

        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUserClaim = _userContext.GetCurrentUser();
            _logger.LogInformation("{ownerEmail} with id {ownderId} Create a new Restaurant {@Restaurant}", currentUserClaim.Email, currentUserClaim.Id, request);
            Restaurant resturant = new Restaurant();
            resturant = _mapper.Map<Restaurant>(request);
            resturant.OwnerId=currentUserClaim.Id;

            int id = await _restaurantRepository.AddRestaurantAsync(resturant);
            return id;
        }
    }
}
