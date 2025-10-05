using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WeatherMonitorReader.Application.Services
{
    /// <summary>
    /// Initializes backround service triggering processing of WeatherMonitor data in fixed intervals
    /// </summary>
    /// <param name="service">A service processing WeatherMonitor data</param>
    /// <param name="runInterval">A <see cref="int"/> defining frequency in minutes</param>
    public class BackgroundReadingService(
        WeatherMonitorReadingService service,
        ILogger<BackgroundReadingService> logger,
        int runInterval
        ) : BackgroundService
    {
        private readonly WeatherMonitorReadingService _service = service;
        private readonly ILogger<BackgroundReadingService> _logger = logger;
        private readonly int _runInterval = runInterval;

        /// <summary>
        /// Starts an execution loop
        /// </summary>
        /// <param name="stoppingToken">A <see cref="CancellationToken"/> used to cancel running task</param>
        /// <returns>A task that represents the background execution loop.</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("[BackgroundReadingService] {time} Request initiated", DateTime.UtcNow);
                await _service.ProcessAsync();
                _logger.LogInformation("[BackgroundReadingService] {time} Request finised", DateTime.UtcNow);
                await Task.Delay(TimeSpan.FromSeconds(_runInterval), stoppingToken);
            }
        }
    }
}