using WeatherMonitorReader.Domain.Enums;
using WeatherMonitorReader.Domain.Models;
using WeatherMonitorReader.Domain.Dtos;

namespace WeatherMonitorReader.Infrastructure.Mappers
{
    internal static class WeatherMonitorSensorMapper
    {
        public static WeatherMonitorSensor MapSensor(WeatherMonitorSensorDto dto, SensorDirection direction)
        {
            int.TryParse(dto.Id, out int sensorId);

            var sensor = new WeatherMonitorSensor()
            {
                Type = dto.Type,
                Name = dto.Name,
                Direction = direction,
                Place=dto.Place,
            };

            if(int.TryParse(dto.Id, out sensorId))
                sensor.SensorId = sensorId;

            return sensor;
        }
    }
}
