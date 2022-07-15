using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Delivery_App.Models
{
    public class Restaurant
    {
        public Guid Id { get; set; }

        public string name { get; set; }

        public string address { get; set; }

        public bool isDeliveryFree { get; set; }

        public virtual ICollection<Food> foodCatalogue{get;set;}
    }
}