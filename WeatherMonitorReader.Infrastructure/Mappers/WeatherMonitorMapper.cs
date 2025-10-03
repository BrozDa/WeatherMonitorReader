using WeatherMonitorReader.Domain.Models;
using WeatherMonitorReader.Domain.Dtos;

namespace WeatherMonitorReader.Infrastructure.Mappers
{
    internal static class WeatherMonitorMapper
    {
        public static WeatherMonitor MapMonitor(WeatherMonitorDto dto)
        {
            WeatherMonitor monitor = new WeatherMonitor()
            {
                DegreeUnit = dto.DegreeUnit,
                PressureUnit = dto.PressureUnit,
                SerialNumber = dto.SerialNumber,
                Model = dto.Model,
                Firmware = dto.Firmware,
                Language = dto.Language,
                R = dto.RFlag,
                Bip = dto.BipFlag,
                PressureType = dto.PressureType
            };

            return monitor;
        }
    }
}
