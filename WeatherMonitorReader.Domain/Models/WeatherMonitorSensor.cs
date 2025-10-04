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

    }
}
