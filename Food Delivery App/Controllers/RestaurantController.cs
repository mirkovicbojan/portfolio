using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Delivery_App.Models;
using Food_Delivery_App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Food_Delivery_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : Controller
    {
        
        private IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService service)
        {
            this._restaurantService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_restaurantService.GetAll());
        }

        [HttpPost]
        public IActionResult Save(Restaurant obj)
        {
            return Ok(_restaurantService.Save(obj));
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_restaurantService.GetOne(id));
        }
        
        [HttpPut]
        public IActionResult UpdateOne(Restaurant request)
        {
            return Ok(_restaurantService.UpdateOne(request));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var forDeletion = _restaurantService.GetOne(id);
            _restaurantService.DeleteOne(forDeletion);
            return Ok("Restaurant successfully deleted.");
        }
    }
}