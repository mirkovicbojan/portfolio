using Microsoft.AspNetCore.Mvc;
using TimeSheet.DTO_models;
using TimeSheet.Models;
using TimeSheet.Services.Interfaces;


namespace TimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        private IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public IActionResult Save(Client obj)
        {
            return Ok(_clientService.Save(obj));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clientService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Client client = _clientService.GetOne(id);
            if (client == null)
            {
                return BadRequest("Client not found");
            }
            return Ok(ClientDTO.ToClientDTO(client));
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var client = _clientService.GetOne(id);
            if (client == null)
            {
                return BadRequest("Client not found");
            }
            _clientService.DeleteOne(client);
            return Ok("Client deleted");
        }

        [HttpPut]
        public ActionResult<Client> UpdateClient(Client request)
        {
            var client = _clientService.GetOne(request.clientID);
            if (client == null)
            {
                return BadRequest("Client not found");
            }
            return _clientService.UpdateOne(request);
        }

    }
}