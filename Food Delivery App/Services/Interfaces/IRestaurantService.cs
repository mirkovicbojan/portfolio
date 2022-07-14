using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Delivery_App.Models;

namespace Food_Delivery_App.Services.Interfaces
{
    public interface IRestaurantService
    {
        public IEnumerable<Restaurant> GetAll();

        public Restaurant GetOne(Guid id);

        public void DeleteOne(Restaurant obj);

        public Restaurant UpdateOne(Restaurant obj);

        public Restaurant Save(Restaurant obj);
    }
}