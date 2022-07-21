using Microsoft.AspNetCore.Mvc;
using Receipt_Micro_Service.Models;
using Receipt_Micro_Service.Services.Interfaces;

namespace Receipt_Micro_Service.Controllers
{
    [Route("api/EmailMicroservice")]
    [ApiController]
    public class ReceiptController:Controller
    {
        private readonly IReceiptService _service;

        public ReceiptController(IReceiptService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult sendReceipt([FromBody]Receipt order)
        {
            _service.emailReceipt(order);
            return Ok();
        }
    }
}