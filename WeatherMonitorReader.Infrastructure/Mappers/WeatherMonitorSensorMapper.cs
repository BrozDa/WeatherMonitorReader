using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Enums;
using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Infrastructure.Mappers
{
    public static class WeatherMonitorSensorMapper
    {
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