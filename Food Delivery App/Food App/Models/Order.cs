using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Delivery_App.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public string userEmail { get; set; }
        
        public double priceSum { get; set; }

        public double deliveryFee { get; set; }

        public double price { get; set; }

        public DateTime createdOn { get; set; }

        public List<string> foods { get; set; }

        public bool isCompleted { get; set; }
    }
}