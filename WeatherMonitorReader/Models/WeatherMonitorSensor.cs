namespace WeatherMonitorReader.Models
{
    public class WeatherMonitorSensor
    {
        public Guid Id { get; set; }
        public Guid WeatherMonitorId { get; set; }
        public WeatherMonitor WeatherMonitor { get; set; } = null!;
        public SensorType Type { get; set; }
        public int SensorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Place {  get; set; }
        public double? Value { get; set; }

    }
}
