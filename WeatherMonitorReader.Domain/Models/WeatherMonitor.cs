using WeatherMonitorReader.Domain.Dtos;

namespace WeatherMonitorReader.Domain.Models
{
    public class WeatherMonitor
    {
        public Guid Id { get; set; }
        public string DegreeUnit { get; set; } = "C";

        public string PressureUnit { get; set; } = "hPa";

        public string SerialNumber { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string Firmware { get; set; } = string.Empty;

        public string Language { get; set; } = string.Empty;

        public string PressureType { get; set; } = string.Empty;
        public string R { get; set; } = string.Empty;
        public string Bip { get; set; } = string.Empty;

        public IEnumerable<WeatherMonitorSensor> Sensors { get; set; } = null!;
        public IEnumerable<WeatherMonitorSnapshot> Snapshots { get; set; } = null!;

        public static WeatherMonitor FromDto(WeatherMonitorDto dto)
        {
            return new WeatherMonitor()
            {
                DegreeUnit = dto.DegreeUnit,
                PressureUnit = dto.PressureUnit,
                SerialNumber = dto.SerialNumber,
                Model = dto.Model,
                Firmware = dto.Firmware,
                Language = dto.Language,
                PressureType = dto.PressureType,
                R = dto.RFlag,
                Bip = dto.BipFlag
            };
        }
    }
}