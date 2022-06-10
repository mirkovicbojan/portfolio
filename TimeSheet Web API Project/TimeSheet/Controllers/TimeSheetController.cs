using Microsoft.AspNetCore.Mvc;
using TimeSheet.DTO_Models;
using TimeSheet.Models;
using TimeSheet.Services.Interfaces;

namespace TimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TimeSheetController : Controller
    {
       
        private readonly ITimeSheetService _timeSheetService;

        public TimeSheetController(ITimeSheetService timeSheetService)
        {
            _timeSheetService = timeSheetService;
        }

        [HttpPost]
        public IActionResult Save(TimeSheetClass obj)
        {

            return Ok(_timeSheetService.Save(obj));
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            TimeSheetClass sheet = _timeSheetService.GetOne(id);
            if(sheet == null)
            {
                return BadRequest("TimeSheet not found");
            }
            return Ok(TimeSheetDTO.ToTimeSheetDTO(sheet));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_timeSheetService.GetAll());
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var timeSheet = _timeSheetService.GetOne(id);
            if(timeSheet == null)
            {
                return BadRequest("TimeSheet with that id wasn't found.");
            }
            _timeSheetService.DeleteOne(timeSheet);
            return Ok("TimeSheet deleted.");
        }

        [HttpPut]
        public IActionResult UpdateOne(TimeSheetClass request)
        {
            var sheet = _timeSheetService.GetOne(request.sheetID);
            if(sheet == null)
            {
                return BadRequest("TimeSheet not found");
            }
            return Ok(_timeSheetService.UpdateOne(request));
        }
    }
}