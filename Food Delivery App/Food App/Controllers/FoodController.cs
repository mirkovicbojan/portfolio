using Food_Delivery_App.DTOModels;
using Food_Delivery_App.Models;
using Food_Delivery_App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Food_Delivery_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : Controller
    {
        private IFoodService _foodService;

        public FoodController(IFoodService service)
        {
            this._foodService = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_foodService.GetAll());
        }

        [HttpPost]
        public IActionResult Save(FoodDTO obj)
        {
            return Ok(_foodService.Save(obj));
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_foodService.GetOne(id));
        }

        [HttpPut]
        public IActionResult UpdateOne(FoodDTO request)
        {
            return Ok(_foodService.UpdateOne(request));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var forDeletion = _foodService.GetOne(id);
            _foodService.DeleteOne(forDeletion);
            return Ok("Food option successfully deleted.");
        }
    }
}