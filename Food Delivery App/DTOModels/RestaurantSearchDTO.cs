using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Delivery_App.DTOModels
{
    public class RestaurantSearchDTO
    {
        public string? name { get; set; }
        
        public bool? isFreeDelivery { get; set; }

        public string? foodName{get;set;}
    }
}