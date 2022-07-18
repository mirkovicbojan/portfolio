using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Delivery_App.Contexts;
using Food_Delivery_App.Models;
using Food_Delivery_App.Repository.Interfaces;

namespace Food_Delivery_App.Repository
{
    public class OrderRepository: Repository<Order>, IOrderRepository
    {
        private readonly FoodAppContext _dbContext;

        public OrderRepository(FoodAppContext context):base(context)
        {
            _dbContext = context;
        }
    }
}