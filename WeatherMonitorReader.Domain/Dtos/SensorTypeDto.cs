using System.Text.Json.Serialization;

namespace WeatherMonitorReader.Domain.Dtos
{
    public class SensorTypeDto
    {
        [JsonPropertyName("sensor")]
        public List<WeatherMonitorSensorDto> Sensors { get; set; } = null!;
    }
}