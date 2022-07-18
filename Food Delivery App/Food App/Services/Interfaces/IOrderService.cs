using Food_Delivery_App.DTOModels;
using Food_Delivery_App.Models;

namespace Food_Delivery_App.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<Order> buildOrder(List<FoodDTO> foodList);

    }
}