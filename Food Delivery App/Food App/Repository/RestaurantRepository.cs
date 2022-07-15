using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Delivery_App.Contexts;
using Food_Delivery_App.Models;
using Food_Delivery_App.Repository.Interfaces;

namespace Food_Delivery_App.Repository
{
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        private FoodAppContext dbContext;

        public RestaurantRepository(FoodAppContext context) : base(context)
        {
            dbContext = context;
        }
    }
}