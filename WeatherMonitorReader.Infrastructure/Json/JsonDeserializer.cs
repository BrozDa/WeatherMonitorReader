using System.Text.Json;
using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Interfaces;

namespace WeatherMonitorReader.Infrastructure.Json
{
    public class JsonDeserializer : IJsonDeserializer
    {
        public XmlRootDto Deserialize(string rawJson)
        {
            var result = JsonSerializer.Deserialize<XmlRootDto>(rawJson);
            return result;
        }
    }
}