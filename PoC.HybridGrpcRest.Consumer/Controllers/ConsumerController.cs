using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using PoC.HybridGrpcRest.Protos.Client;

namespace PoC.HybridGrpcRest.Consumer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        public ConsumerController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            HttpClient clientHttp = _clientFactory.CreateClient("MyHttpClient");
            HttpResponseMessage resp = await clientHttp.GetAsync("status/200");

            if (resp.IsSuccessStatusCode)
            {
                return Ok(resp.StatusCode);
            }
            else
            {
                return BadRequest(resp.StatusCode);
            }
        }

        [HttpGet("grpc")]
        public async Task<IActionResult> GetGrpc()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5002");
            var client = new Status.StatusClient(channel);
            var reply = await client.GetStatusAsync(new StatusRequest { StatusCode = 200 });

            if (reply.StatusCode >= 200 && reply.StatusCode <= 299)
            {
                return Ok(reply.StatusCode);
            }
            else
            {
                return BadRequest(reply.StatusCode);
            }
        }
    }
}