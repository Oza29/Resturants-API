using MediatR;

namespace Core.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommand:IRequest
    {
        public DeleteRestaurantCommand(int id)
        {
            ID= id;
        }
      public int ID { get; set; }
    }
}
