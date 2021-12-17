using Grpc.Core;
using IotRestFullApi.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace IotRestFullApi
{
    public class StatsStreamingService : StatsStreaming.StatsStreamingBase
    {
        private readonly ILogger<StatsStreamingService> _logger;
        private readonly StatsRepository statsRepository;

        public StatsStreamingService(ILogger<StatsStreamingService> logger, StatsRepository statsRepository)
        {
            _logger = logger;
            this.statsRepository = statsRepository;
        }

        public override async Task Statistics(RequestStatsStream request, IServerStreamWriter<ResponseStatsStream> responseStream, ServerCallContext context)
        {
            _logger.LogDebug("Grpc client request...");
            //TODO get device from request and print the stream of "stats" for the device from -500 last records
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await responseStream.WriteAsync(new ResponseStatsStream { DeviceId = request.DeviceId });
                await Task.Delay(1000);
            }
        }
    }
}
