using Grpc.Core;
using static PoC.HybridGrpcRest.Protos.Service.Status;

namespace PoC.HybridGrpcRest.Service.Services
{
    public interface IStatusService
    {
        Task<StatusResponse> GetStatus(StatusRequest status);
    }

    public class StatusService : StatusBase, IStatusService
    {
        public override Task<Protos.Service.StatusResponse> GetStatus(Protos.Service.StatusRequest status, ServerCallContext context)
        {
            StatusResponse response = GetStatus(new StatusRequest() { StatusCode = status.StatusCode }).Result;

            return Task.FromResult(new Protos.Service.StatusResponse()
            {
                StatusCode = response.StatusCode
            });
        }

        public Task<StatusResponse> GetStatus(StatusRequest status)
        {
            int statusCode = status.StatusCode + 10;
            return Task.FromResult(new StatusResponse()
            {
                StatusCode = statusCode
            });
        }
    }

    public class StatusRequest
    {
        public int StatusCode { get; set; }
    }

    public class StatusResponse
    {
        public int StatusCode { get; set; }
    }
}
