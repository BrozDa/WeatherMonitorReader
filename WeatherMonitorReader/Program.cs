using WeatherMonitorReader.Application.Services;
using WeatherMonitorReader.Infrastructure.Json;
using WeatherMonitorReader.Infrastructure.Xml;
using WeatherMonitorReader.Infrastructure.Persistence.Repositories;
using WeatherMonitorReader.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace WeatherMonitorReader
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                .AddConsole()
                .SetMinimumLevel(LogLevel.Information);
            });
            ILogger<WeatherMonitorReadingService> svcLogger = loggerFactory.CreateLogger<WeatherMonitorReadingService>();



            JsonDeserializer deserializer = new();
            XmlFetcher fetcher = new();
            XmlToJsonConverter converter = new();

            var contextFactory = new WeatherMonitorContextFactory();

            var context = contextFactory.CreateDbContext(null);

            await context.Database.EnsureCreatedAsync();


            WeatherMonitorRepository repo = new(context);


            WeatherMonitorReadingService service = new(
                fetcher,
                converter,
                deserializer,
                repo,
                svcLogger);

            await service.ProcessAsync();
        }
    }
}
