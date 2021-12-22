using Grpc.Core;
using IotRestFullApi.Dto;
using IotRestFullApi.Repositories;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace IotRestFullApi
{
    public class StatsStreamingService : StatsStreaming.StatsStreamingBase
    {
        private readonly ILogger<StatsStreamingService> _logger;
        private readonly StatsRepository statsRepository;

        public StatsStreamingService(StatsRepository statsRepository,ILogger<StatsStreamingService> logger)
        {
            _logger = logger;
            this.statsRepository = statsRepository;
            _logger.LogDebug("Grpc load..");
        }

        public override async Task Statistics(RequestStatsStream request, IServerStreamWriter<ResponseStatsStream> responseStream, ServerCallContext context)
        {
            _logger.LogDebug("Grpc client request...");
            if (request.DeviceId != null)
            {
                while (!context.CancellationToken.IsCancellationRequested)
                {
                    StatsResponse response = statsRepository.GetAll()
                        .Where(_ => _.DeviceID == request.DeviceId).ToList().LastOrDefault();
                    if (response != null)
                        await responseStream.WriteAsync(new ResponseStatsStream { DeviceId = request.DeviceId, Payload = response.Payload });
                    await Task.Delay(2000);
                }
            }
        }
    }
}
