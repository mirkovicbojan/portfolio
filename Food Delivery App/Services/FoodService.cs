using Food_Delivery_App.DTOModels;
using Food_Delivery_App.Models;
using Food_Delivery_App.Repository.Interfaces;
using Food_Delivery_App.Services.Interfaces;

namespace Food_Delivery_App.Services
{
    public class FoodService:IFoodService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public FoodService(IFoodRepository repository, IRestaurantRepository restaurantRepository)
        {
            this._foodRepository = repository;
            this._restaurantRepository = restaurantRepository;
        }

        public IEnumerable<FoodDTO> GetAll()
        {
            var retVal = _foodRepository.GetAll();
            return retVal.Select(item => FoodDTO.toFoodDTO(item));
        }

        public Food GetOne(Guid id)
        {
            var retVal = _foodRepository.GetById(id);
            return retVal;
        }

        public void DeleteOne(Food obj)
        {
            var retVal = _foodRepository.GetById(obj.Id);

            _foodRepository.Delete(retVal);
        }

        public Food UpdateOne(FoodDTO obj)
        {
            var retVal = _foodRepository.GetById(obj.Id);
            retVal.name = obj.name;
            retVal.price = obj.price;
            return _foodRepository.Edit(retVal);
        }

        public Food Save(FoodDTO obj)
        {
            var food = new Food();
            food.Id = obj.Id;
            food.name = obj.name;
            food.price = obj.price;
            food.restaurantId = obj.restaurantId;
            // food.restaurant = _restaurantRepository.GetById(food.restaurantId);
            
            return _foodRepository.Save(food);
        }
    }
}