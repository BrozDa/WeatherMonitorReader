namespace WeatherMonitorReader.Models
{
    public class WeatherMonitorSnapShotMinMax
    {
        public Guid Id { get; set; }
        public Guid SensorId { get; set; }
        public WeatherMonitorSensor Sensor { get; set; } = null!;
        public Guid SnapShotId { get; set; }
        public WeatherMonitorSnapshot WeatherMonitorSnapshot { get; set; } = null!;
        public double Min { get; set; }
        public double Max { get; set; }
    }
}
