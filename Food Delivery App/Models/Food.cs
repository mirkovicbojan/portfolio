using System.ComponentModel.DataAnnotations.Schema;


namespace Food_Delivery_App.Models
{
    public class Food
    {
        public Guid Id { get; set; }

        public string name { get; set; }

        public float price { get; set; }

        public virtual Restaurant restaurant { get; set; }
    }
}