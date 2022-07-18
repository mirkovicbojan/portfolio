using Food_Delivery_App.DTOModels;
using Food_Delivery_App.Models;
using Food_Delivery_App.Repository.Interfaces;
using Food_Delivery_App.Services.Interfaces;

namespace Food_Delivery_App.Services
{
    public class OrderService : IOrderService
    {
        public readonly IOrderRepository _orderRepository;

        public readonly IRestaurantRepository _restaurantRepository;

        private readonly IHttpClientFactory _http;

        public OrderService(IOrderRepository orderRepository, IRestaurantRepository restaurantRepository, IHttpClientFactory http)
        {
            this._orderRepository = orderRepository;
            this._restaurantRepository = restaurantRepository;
            this._http = http;
        }

        public async Task<Order> buildOrder(List<FoodDTO> foodList)
        {
            var restaurant = _restaurantRepository.GetById(foodList[0].restaurantId);

            var order = new Order();
            order.orderId = new Guid();
            if(restaurant.isDeliveryFree)
            {
                order.deliveryFee = 0;
            }
            order.createdOn = DateTime.UtcNow;
            order.foods = new List<string>();
            foreach(var food in foodList)
            {
                order.foods.Add($"{food.name} : {food.price}");
                order.price += food.price;
            }
            order.priceSum = order.price + order.deliveryFee;
            order.isCompleted = false;

            _orderRepository.Save(order);

            return order;
        }

    }
}