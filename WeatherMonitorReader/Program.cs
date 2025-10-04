using WeatherMonitorReader.Application;
using WeatherMonitorReader.Application.Services;
using WeatherMonitorReader.Infrastructure.Json;
using WeatherMonitorReader.Infrastructure.Xml;
using WeatherMonitorReader.Infrastructure.Persistence.Repositories;
using WeatherMonitorReader.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Internal;

namespace WeatherMonitorReader
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            JsonDeserializer deserializer = new();
            XmlFetcher fetcher = new();
            XmlToJsonConverter converter = new();

            var contextFactory = new WeatherMonitorContextFactory();

            var context = contextFactory.CreateDbContext(null);

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();


            WeatherMonitorRepository repo = new(context);


            WeatherMonitorReadingService service = new(
                fetcher,
                converter,
                deserializer,
                repo);

            await service.ProcessAsync();
        }
    }
}
