
using Food_Delivery_App.Models;

namespace Food_Delivery_App.DTOModels
{
    public class FoodDTO
    {
        public Guid Id { get; set; }

        public string name { get; set; }

        public float price { get; set; }

        public Guid? restaurantId { get; set; }

        public static FoodDTO toFoodDTO(Food obj)
        {
            FoodDTO food = new FoodDTO();
            food.Id = obj.Id;
            food.name = obj.name;
            food.price = obj.price;
            food.restaurantId = obj.restaurantId;
            return food;
        }
    }
}