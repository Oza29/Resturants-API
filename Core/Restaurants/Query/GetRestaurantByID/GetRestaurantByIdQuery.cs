using Core.Restaurants.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Restaurants.Query.GetRestaurantByID
{
    public class GetRestaurantByIdQuery:IRequest<RestaurantDTO>
    {
        public GetRestaurantByIdQuery(int id )
        {
            ID=id;
        }
        public int ID {  get; set; }
    }
}
