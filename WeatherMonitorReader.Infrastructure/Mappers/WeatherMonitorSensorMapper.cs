using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Enums;
using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Infrastructure.Mappers
{
    /// <summary>
    /// Provides functionality to map <see cref="WeatherMonitorSensorDto"/> into <see cref="WeatherMonitorSensor"/>
    /// </summary>
    public static class WeatherMonitorSensorMapper
    {
        /// <summary>
        /// Maps <see cref="WeatherMonitorSensorDto"/> into <see cref="WeatherMonitorSensor"/>
        /// </summary>
        /// <param name="dto">Dto containing data used for mapping</param>
        /// <param name="direction">A <see cref="SensorDirection"/> confirming sensor direction</param>
        /// <param name="monitorId">A <see cref="Guid"/> defining monitor to which sensor is associated to</param>
        /// <returns>A mapped <see cref="WeatherMonitorSensor"/></returns>
        public static WeatherMonitorSensor Map(WeatherMonitorSensorDto dto, SensorDirection direction, Guid monitorId)
        {
            int.TryParse(dto.Id, out int sensorId);

            var sensor = new WeatherMonitorSensor()
            {
                Type = dto.Type,
                Name = dto.Name,
                Direction = direction,
                Place = dto.Place,
            };

            if (int.TryParse(dto.Id, out sensorId))
                sensor.SensorId = sensorId;

            sensor.WeatherMonitorId = monitorId;
            return sensor;
        }
    }
}