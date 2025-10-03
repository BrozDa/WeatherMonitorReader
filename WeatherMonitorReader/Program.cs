using WeatherMonitorReader.Application;
using WeatherMonitorReader.Application.Services;
using WeatherMonitorReader.Infrastructure.Json;
using WeatherMonitorReader.Infrastructure.Xml;

namespace WeatherMonitorReader
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            JsonDeserializer deserializer = new();
            XmlFetcher fetcher = new();
            XmlToJsonConverter converter = new();

            WeatherMonitorReadingService service = new(
                fetcher,
                converter,
                deserializer );

            await service.ProcessAsync();
        }
    }
}
