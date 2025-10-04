namespace WeatherMonitorReader.Domain.Models
{
    public class WeatherMonitorSnapshot
    {
        public Guid Id { get; set; }
        public Guid WeatherMonitorId { get; set; }
        public WeatherMonitor WeatherMonitor { get; set; } = null!;
        public bool IsMonitorResponding { get; set; } = true;
        public int? Runtime { get; set; }
        public int? Freemem { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public WeatherMonitorVariables? WeatherMonitorVariables { get; set; } = null!;
        public IEnumerable<WeatherMonitorSensorReading>? SensorReadings { get; set; } = null!;
        public IEnumerable<WeatherMonitorSnapshotMinMax>? SensorMinMaxes { get; set; } = null!;

        public static WeatherMonitorSnapshot MonitorDownSnapshot(Guid monitorId)
        {
            return new WeatherMonitorSnapshot()
            {
                WeatherMonitorId = monitorId,
                IsMonitorResponding = false,
                Runtime = null,
                Freemem = null,
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                Time = TimeOnly.FromDateTime(DateTime.UtcNow),
                WeatherMonitorVariables = null,
                SensorReadings = null,
                SensorMinMaxes = null,
            };
        }

    }
}
