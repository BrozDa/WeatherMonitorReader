using WeatherMonitorReader.Domain.Dtos;

namespace WeatherMonitorReader.Domain.Interfaces
{
    /// <summary>
    /// Represents contract for deserializing JSON string into structured object
    /// </summary>
    public interface IJsonDeserializer
    {
        /// <summary>
        /// Deserializes a raw JSON string to the <see cref="XmlRootDto"/> object.
        /// </summary>
        /// <param name="rawJson">A <see cref="string"/> containing JSON to be deserialized</param>
        /// <returns>A<see cref="XmlRootDto"/> representing the deserialized data.</returns>
        public XmlRootDto Deserialize(string rawJson);
    }
}