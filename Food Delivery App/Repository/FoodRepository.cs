using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Delivery_App.Contexts;
using Food_Delivery_App.Models;
using Food_Delivery_App.Repository.Interfaces;

namespace Food_Delivery_App.Repository
{
    public class FoodRepository : Repository<Food>, IFoodRepository
    {
        private FoodAppContext dbContext;

        public FoodRepository(FoodAppContext context) : base(context)
        {
            dbContext = context;
        }
    }
}