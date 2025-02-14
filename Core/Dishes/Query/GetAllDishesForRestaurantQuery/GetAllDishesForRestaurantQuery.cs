using Core.Dishes.Dtos;
using MediatR;

namespace Core.Dishes.Query.GetAllDishesForRestaurantQuery
{
    public class GetAllDishesForRestaurantQuery : IRequest<IEnumerable<DishDTO>>
    {
        public GetAllDishesForRestaurantQuery(int RestaurantId)
        {
            Id = RestaurantId;
        }
        public int Id { get; set; }
    }
}
