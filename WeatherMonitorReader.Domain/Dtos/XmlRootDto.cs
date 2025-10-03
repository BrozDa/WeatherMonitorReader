using System.Text.Json.Serialization;

namespace WeatherMonitorReader.Domain.Dtos
{
    public class XmlRootDto
    {
        [JsonPropertyName("?xml")]
        public XmlMetadataDto Metadata { get; set; } = null!;

        [JsonPropertyName("wario")]
        public WeatherMonitorDto Device { get; set; } = null!;
    }
}
