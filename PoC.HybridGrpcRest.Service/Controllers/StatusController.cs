using Microsoft.AspNetCore.Mvc;
using PoC.HybridGrpcRest.Service.Services;

namespace PoC.HybridGrpcRest.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _clientService;
        public StatusController(IStatusService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("{statusCode}")]
        public async Task<IActionResult> Get([FromRoute] int statusCode)
        {
            var request = new StatusRequest() { StatusCode = statusCode };
            StatusResponse response = await _clientService.GetStatus(request);
            return StatusCode(response.StatusCode);
        }
    }
}