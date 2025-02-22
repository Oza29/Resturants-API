using Core.Dishes.Dtos;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Restaurants.Dtos
{
    public class RestaurantDTO
    {
        public int ID { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;

        public bool HasDelivery { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public List<DishDTO> Dishes { get; set; } = new();

    
        
    }
}
