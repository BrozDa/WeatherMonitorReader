using Microsoft.Extensions.Hosting;

namespace WeatherMonitorReader.Application.Services
{
    public class BackgroundReadingService(
        WeatherMonitorReadingService service,
        int runInterval
        ) : BackgroundService
    {

        private readonly WeatherMonitorReadingService _service = service;
        private readonly int _runInterval = runInterval;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _service.ProcessAsync();
                await Task.Delay(TimeSpan.FromMinutes(_runInterval), stoppingToken);
            }
        }
    }
}
