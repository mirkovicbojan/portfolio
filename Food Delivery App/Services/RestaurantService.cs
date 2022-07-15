using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Delivery_App.DTOModels;
using Food_Delivery_App.Models;
using Food_Delivery_App.Repository.Interfaces;
using Food_Delivery_App.Services.Interfaces;

namespace Food_Delivery_App.Services
{
    public class RestaurantService:IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository repository)
        {
            this._restaurantRepository = repository;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            var retVal = _restaurantRepository.GetAll();
            return retVal;
        }

        public Restaurant GetOne(Guid id)
        {
            var retVal = _restaurantRepository.GetById(id);
            return retVal;
        }

        public void DeleteOne(Restaurant obj)
        {
            var retVal = _restaurantRepository.GetById(obj.Id);

            _restaurantRepository.Delete(retVal);
        }

        public Restaurant UpdateOne(Restaurant obj)
        {
            var retVal = _restaurantRepository.GetById(obj.Id);

            return _restaurantRepository.Edit(retVal);
        }

        public Restaurant Save(Restaurant obj)
        {
            return _restaurantRepository.Save(obj);
        }

        public IEnumerable<FoodDTO> showCatalogue(Guid id)
        {
            var currentRestaurant = _restaurantRepository.GetById(id);
            return currentRestaurant.foodCatalogue.Select(item => FoodDTO.toFoodDTO(item));
        } 
        public IEnumerable<Restaurant> filteredSearch(RestaurantSearchDTO obj)
        {
            var restaurantList = _restaurantRepository.GetAll();
            if(!string.IsNullOrEmpty(obj.name))
            {
                restaurantList.Where(restaurant => restaurant.name == obj.name);
            }
            if(!string.IsNullOrEmpty(obj.foodName))
            {
                restaurantList.Where(restaurant => restaurant.foodCatalogue.Where(food => food.name == obj.foodName) != null);
            }
            if(obj.isFreeDelivery == true)
            {
                restaurantList.Where(restaurant => restaurant.isDeliveryFree == true);
            }

            return restaurantList;
        }
    }
}