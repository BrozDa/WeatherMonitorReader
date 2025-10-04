using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Enums;

namespace WeatherMonitorReader.Domain.Models
{
    public class WeatherMonitorSensor
    {
        public Guid Id { get; set; }
        public Guid WeatherMonitorId { get; set; }
        public WeatherMonitor WeatherMonitor { get; set; } = null!;
        public string Type { get; set; } = string.Empty;
        public int SensorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Place {  get; set; } = string.Empty;
        public SensorDirection Direction {  get; set; }
        public IEnumerable<WeatherMonitorSensorReading> SensorReadings { get; set; } = null!;
        public IEnumerable<WeatherMonitorSnapshotMinMax> SensorMinMaxes { get; set; } = null!;

        public static WeatherMonitorSensor FromDto(WeatherMonitorSensorDto dto, SensorDirection direction) => 
            new WeatherMonitorSensor()
            {
                Type = dto.Type,
                SensorId = dto.Id,
            }
        /*
         * [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("place")]
        public string Place { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;
         */
    }
}
