
namespace Receipt_Micro_Service.Models
{
    public class Receipt
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