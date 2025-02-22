using AutoMapper;
using Core.RepositoryContracts;
using Core.Restaurants.Dtos;
using Domain.Entites;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Restaurants.Query.GetRestaurantByID
{
    public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDTO>
    {
        private readonly ILogger<GetRestaurantByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;
        public GetRestaurantByIdQueryHandler(IRestaurantRepository repository, ILogger<GetRestaurantByIdQueryHandler> logger, IMapper mapper)
        {
            _restaurantRepository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<RestaurantDTO> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("getting  restaurant {RestaurantID}",request.ID);
            Restaurant restaurant = await _restaurantRepository.GetRestaurantAsync(request.ID)??
                throw new NotFoundException(nameof(Restaurant),request.ID.ToString());
            var resturantDTO = _mapper.Map<RestaurantDTO>(restaurant);
            return resturantDTO;
        }
    }
}
