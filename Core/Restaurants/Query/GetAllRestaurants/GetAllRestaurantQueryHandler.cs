using AutoMapper;
using Core.Common;
using Core.RepositoryContracts;
using Core.Restaurants.Dtos;
using Domain.Entites;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Restaurants.Query.GetAllRestaurants
{
    public class GetAllRestaurantQueryHandler : IRequestHandler<GetAllRestaurantQuery, PagedResult<RestaurantDTO>>
    {
        private readonly ILogger<GetAllRestaurantQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;
        public GetAllRestaurantQueryHandler(IRestaurantRepository repository, ILogger<GetAllRestaurantQueryHandler> logger, IMapper mapper)
        {
            _restaurantRepository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task <PagedResult<RestaurantDTO>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("getting all restaurants");
            IEnumerable<Restaurant> resturants = new List<Restaurant>();
               var (restaurants,totalCount) = await _restaurantRepository.GetMatchedResultsAsync(request.searchPharse,
                    request.PageSize,request.PageNumber);
                
            var restaurantsDTO = _mapper.Map<IEnumerable<RestaurantDTO>>(resturants);
           var result = new PagedResult<RestaurantDTO>(request.PageSize,request.PageNumber, totalCount,restaurantsDTO);
            return result;
        }
    }
}
