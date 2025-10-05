namespace WeatherMonitorReader.Domain.Models
{
    public class WeatherMonitorSnapshotMinMax
    {
        public Guid Id { get; set; }
        public Guid SensorId { get; set; }
        public WeatherMonitorSensor Sensor { get; set; } = null!;
        public Guid SnapshotId { get; set; }
        public WeatherMonitorSnapshot Snapshot { get; set; } = null!;
        public double Min { get; set; }
        public double Max { get; set; }
    }
}