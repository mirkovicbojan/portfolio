using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeSheet.Models;
using TimeSheet.Services.Interfaces;

namespace TimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_categoryService.GetAll());
        }

        [Authorize]
        [HttpPost]
        public IActionResult Save(Category obj)
        {
            return Ok(obj);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_categoryService.GetOne(id));
        }
        
        [Authorize]
        [HttpPut]
        public IActionResult UpdateOne(Category request)
        {
            return Ok(_categoryService.UpdateOne(request));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.DeleteOne(id);
            return Ok("Category successfully deleted.");
        }
    }
}