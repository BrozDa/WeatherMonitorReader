
using System.Text.Json.Serialization;

namespace WeatherMonitorReader.Domain.Dtos
{
    public class WeatherMonitorSnapshotMinMaxDto
    {
        [JsonPropertyName("@id")]
        public string SensorId { get; set; } = string.Empty;

        [JsonPropertyName("@min")]
        public string Min {  get; set; } = string.Empty;

        [JsonPropertyName("@max")]
        public string Max { get; set; } = string.Empty;

    }
}
