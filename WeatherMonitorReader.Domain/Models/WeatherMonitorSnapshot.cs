using System;

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
            var date = DateTime.UtcNow;
            date = new DateTime((date.Ticks / TimeSpan.TicksPerSecond) * TimeSpan.TicksPerSecond); //props to stackoverflow

            return new WeatherMonitorSnapshot()
            {
                WeatherMonitorId = monitorId,
                IsMonitorResponding = false,
                Runtime = null,
                Freemem = null,
                Date = DateOnly.FromDateTime(date),
                Time = TimeOnly.FromDateTime(date),
                WeatherMonitorVariables = null,
                SensorReadings = null,
                SensorMinMaxes = null,
            };
        }
    }
}