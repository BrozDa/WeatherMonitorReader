using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Infrastructure.Mappers
{
    /// <summary>
    /// Provides functionality to map <see cref="WeatherMonitorDto"/> into <see cref="WeatherMonitor"/>
    /// </summary>
    public static class WeatherMonitorMapper
    {
        /// <summary>
        /// Maps <see cref="WeatherMonitorDto"/> into <see cref="WeatherMonitor"/>
        /// </summary>
        /// <param name="dto">Dto containing data used for mapping</param>
        /// <returns>A mapped <see cref="WeatherMonitor"/></returns>
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