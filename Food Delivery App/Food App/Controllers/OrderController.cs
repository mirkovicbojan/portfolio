using Food_Delivery_App.DTOModels;
using Food_Delivery_App.Models;
using Food_Delivery_App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Food_Delivery_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        public readonly IOrderService _orderService;

        public OrderController(IOrderService service)
        {
            this._orderService = service;
        }

        [HttpPost]
        public IActionResult SubmitOrder([FromBody]List<FoodDTO> foodList, string userEmail)
        {
            return Ok(_orderService.buildOrder(foodList, userEmail));
        }
    }
}