using AutoMapper;
using Core.Dishes.Dtos;
using Core.Dishes.Query.GetAllDishesForRestaurantQuery;
using Core.RepositoryContracts;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites;

namespace Core.Dishes.Query.GetDishForRestaurantQuery
{
    public class GetDishForRestaurantQueryHandler : IRequestHandler<GetDishForRestaurantQuery, DishDTO>
    {
        private readonly ILogger<GetDishForRestaurantQueryHandler> _logger;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public GetDishForRestaurantQueryHandler(ILogger<GetDishForRestaurantQueryHandler> logger,
         IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
        }
        public async Task<DishDTO> Handle(GetDishForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting  dish {dishId} from restaurant {restaurantId}", request.DishId, request.RestaurantId);
            var restaurant =await  _restaurantRepository.GetRestaurantAsync(request.RestaurantId);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            var dish = restaurant.Dishes.FirstOrDefault(n => n.ID == request.DishId);
            if(dish==null)
            throw new NotFoundException(nameof(Dish), request.DishId.ToString());

            var result =  _mapper.Map<DishDTO>(dish);
            return result;
        }
    }
}
