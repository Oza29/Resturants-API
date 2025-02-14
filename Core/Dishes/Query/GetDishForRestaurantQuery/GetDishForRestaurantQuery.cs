using Core.Dishes.Dtos;
using MediatR;

namespace Core.Dishes.Query.GetDishForRestaurantQuery
{
    public class GetDishForRestaurantQuery:IRequest<DishDTO>
    {
        public GetDishForRestaurantQuery(int restaurantId,int dishId)
        {
            RestaurantId= restaurantId;
            DishId= dishId;
        }
        public int RestaurantId { get; set; }
        public int DishId { get; set; }
    }
}
