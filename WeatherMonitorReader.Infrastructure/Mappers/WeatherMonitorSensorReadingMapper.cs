using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Infrastructure.Mappers
{
    /// <summary>
    /// Provides functionality to map <see cref="WeatherMonitorSensorDto"/> into <see cref="WeatherMonitorSensorReading"/>
    /// </summary>
    public static class WeatherMonitorSensorReadingMapper
    {
        /// <summary>
        /// Maps <see cref="WeatherMonitorSensorDto"/> into <see cref="WeatherMonitorSensorReading"/>
        /// </summary>
        /// <param name="dto">Dto containing data used for mapping</param>
        /// <param name="sensorId">A <see cref="Guid"/> defining <see cref="WeatherMonitorSensor"/> to which reading is associated to</param>
        /// <param name="snapshotId">A <see cref="Guid"/> defining <see cref="WeatherMonitorSnapshot"/> to which reading is associated to</param>
        /// <returns>A mapped <see cref="WeatherMonitorSensorReading"/></returns>
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