using MediatR;

namespace Core.Dishes.Command.DeleteAllDishes
{
    public class DeleteAllDishesFromResturantCommand:IRequest
    {
        public DeleteAllDishesFromResturantCommand(int restaurantId)
        {
            RestaurantId = restaurantId;
        }
        public int RestaurantId { get; set; }
    }
}
