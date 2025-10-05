using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Infrastructure.Mappers
{
    public static class WeatherMonitorSensorReadingMapper
    {
        public static WeatherMonitorSensorReading Map(WeatherMonitorSensorDto dto, Guid sensorId, Guid snapshotId)
        {
            var reading = new WeatherMonitorSensorReading();

            reading.SensorId = sensorId;
            reading.SnapshotId = snapshotId;

            reading.Place = dto.Place;

            if (double.TryParse(dto.Value, out double value))
                reading.Value = value;

            return reading;
        }
    }
}