using Microsoft.Extensions.Hosting;

namespace WeatherMonitorReader.Application.Services
{
    /// <summary>
    /// Initializes backround service triggering processing of WeatherMonitor data in fixed intervals
    /// </summary>
    /// <param name="service">A service processing WeatherMonitor data</param>
    /// <param name="runInterval">A <see cref="int"/> defining frequency in minutes</param>
    public class BackgroundReadingService(
        WeatherMonitorReadingService service,
        int runInterval
        ) : BackgroundService
    {
        private readonly WeatherMonitorReadingService _service = service;
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
                await _service.ProcessAsync();
                await Task.Delay(TimeSpan.FromMinutes(_runInterval), stoppingToken);
            }
        }
    }
}