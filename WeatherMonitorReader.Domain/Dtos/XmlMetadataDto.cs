using System.Text.Json.Serialization;

namespace WeatherMonitorReader.Domain.Dtos
{
    public class XmlMetadataDto
    {
        [JsonPropertyName("@version")]
        public string Version { get; set; } = string.Empty;

        [JsonPropertyName("@encoding")]
        public string Encoding { get; set; } = string.Empty;
    }
}