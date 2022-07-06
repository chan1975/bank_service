using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace client.api.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientController: ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Get all clients");
            return Ok(new {message = "Cliente listado"});
        }

    }
}