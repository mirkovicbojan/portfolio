using Food_Delivery_App.DTOModels;
using Food_Delivery_App.Models;

namespace Food_Delivery_App.Services.Interfaces
{
    public interface IFoodService
    {
        public IEnumerable<FoodDTO> GetAll();

        public Food GetOne(Guid id);

        public void DeleteOne(Food obj);

        public Food UpdateOne(FoodDTO obj);

        public Food Save(FoodDTO obj);
    }
}