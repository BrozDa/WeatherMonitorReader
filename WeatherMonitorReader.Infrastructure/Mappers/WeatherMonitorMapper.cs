using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Infrastructure.Mappers
{
    public static class WeatherMonitorMapper
    {
        public static WeatherMonitor Map(WeatherMonitorDto dto)
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