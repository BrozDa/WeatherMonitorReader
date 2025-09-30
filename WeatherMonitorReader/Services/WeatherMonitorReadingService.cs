using System.Text.Json;
using System.Xml;
using WeatherMonitorReader.Domain.Interfaces;
using WeatherMonitorReader.Infrastructure.Json;

namespace WeatherMonitorReader.Domain.Services
{
    internal class WeatherMonitorReadingService(
        IXmlFetcher fetcher,
        IXmlToJsonConverter converter)
    {
        public async Task FetchDataAsync()
        {
            XmlDocument doc = await fetcher.FetchXmlDocumentAsync();

            string json = converter.ConvertXmlToJson(doc);

            Console.WriteLine(json);

            JsonDeserializer deserializer = new();
            deserializer.Deserialize(json);

            Console.WriteLine("a");
        }
    }
}
