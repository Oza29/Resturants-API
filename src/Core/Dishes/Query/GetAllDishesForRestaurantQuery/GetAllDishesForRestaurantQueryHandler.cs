using AutoMapper;
using Core.Dishes.Command.CreateDish;
using Core.Dishes.Dtos;
using Core.RepositoryContracts;
using Domain.Entites;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Dishes.Query.GetAllDishesForRestaurantQuery
{
    public class GetAllDishesForRestaurantQueryHandler : IRequestHandler<GetAllDishesForRestaurantQuery, IEnumerable<DishDTO>>
    {
        private readonly ILogger<GetAllDishesForRestaurantQueryHandler> _logger;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public GetAllDishesForRestaurantQueryHandler(ILogger<GetAllDishesForRestaurantQueryHandler> logger,
         IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
        }
        public async Task<IEnumerable<DishDTO>> Handle(GetAllDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all dishes from restaurant {restaurantId}",request.Id);
            Restaurant? restaurant = await _restaurantRepository.GetRestaurantAsync(request.Id);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            var dishes = _mapper.Map<IEnumerable<DishDTO>>(restaurant.Dishes);
            return dishes;
        }
    }
}
