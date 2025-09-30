namespace WeatherMonitorReader.Models
{
    public class WeatherMonitorSensorReading
    {
        public Guid Id { get; set; }
        public Guid SensorId { get; set; }
        public WeatherMonitorSensor Sensor { get; set; } = null!;

        public Guid SnapshotId { get; set; }
        public WeatherMonitorSnapshot Snapshot { get; set; } = null!;
        public string? Place { get; set; }
        public double? Value { get; set; }
        
    }
}
