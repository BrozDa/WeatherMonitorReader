using System.Text.Json;
using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Interfaces;

namespace WeatherMonitorReader.Infrastructure.Json
{
    /// <summary>
    /// Provides functionality to deserialize JSON strings into <see cref="XmlRootDto"/>
    /// </summary>
    /// <remarks>
    /// Implements <see cref="IJsonDeserializer"/>
    /// </remarks>
    public class JsonDeserializer : IJsonDeserializer
    {
        /// <inheritdoc/>
        public XmlRootDto Deserialize(string rawJson)
        {
            var result = JsonSerializer.Deserialize<XmlRootDto>(rawJson);
            return result;
        }
    }
}