using WeatherMonitorReader.Domain.Interfaces;
using WeatherMonitorReader.Domain.Services;
using WeatherMonitorReader.Infrastructure.Xml;

namespace WeatherMonitorReader
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            XmlFetcher fetcher = new();
            XmlToJsonConverter converter = new();

            WeatherMonitorReadingService svc = new(fetcher, converter);

            await svc.FetchDataAsync();



        }
    }
}
