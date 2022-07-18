using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Delivery_App.DTOModels;

namespace Food_Delivery_App.Models
{
    public class Order
    {
        public Guid orderId { get; set; }

        public double priceSum { get; set; }

        public double deliveryFee { get; set; }

        public double price { get; set; }

        public DateTime createdOn { get; set; }

        public List<string> foods { get; set; }

        public bool isCompleted { get; set; }
    }
}