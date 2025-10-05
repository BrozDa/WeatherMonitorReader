using WeatherMonitorReader.Domain.Dtos;

namespace WeatherMonitorReader.Domain.Interfaces
{
    public interface IJsonDeserializer
    {
        public XmlRootDto Deserialize(string rawJson);
    }
}