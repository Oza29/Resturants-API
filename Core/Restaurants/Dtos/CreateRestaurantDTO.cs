﻿using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Restaurants.Dtos
{
    public class CreateRestaurantDto
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public bool HasDelivery { get; set; }

        public string? ContactEmail { get; set; }

        public string? ContactNumber { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
      
        public string? PostalCode { get; set; }


    }
    
}
