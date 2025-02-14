using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommand:IRequest
    {
       public int ID { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool HasDelivery {  get; set; }
    }
}
