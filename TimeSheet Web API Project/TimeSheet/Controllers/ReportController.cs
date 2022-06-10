using Microsoft.AspNetCore.Mvc;
using TimeSheet.DTO_Models;
using TimeSheet.Services.Interfaces;


namespace TimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;


        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        public IActionResult Search([FromBody] ReportSearchDTO obj)
        {
            var reports = _reportService.Search(obj);
            return Ok(reports);
        }

        [HttpPost("generatePDF")]
        public IActionResult generatePDF([FromBody] ReportSearchDTO obj)
        {
            var reports = _reportService.Search(obj);
            Response.Headers.Add("Content-Disposition", @"attachment;filename=""Reports.pdf""");
            return File(_reportService.writeToPDF(reports), "application/pdf");
        }

        [HttpPost("writeToCSV")]
        public IActionResult writeToCsv([FromBody] ReportSearchDTO obj)
        {
            var reports = _reportService.Search(obj);
            Response.Headers.Add("Content-Disposition", @"attachment;filename=""Reports.csv""");
            return File(_reportService.writeToCSV(reports), "text/csv");
        }
    }
}