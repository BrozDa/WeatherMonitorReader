namespace WeatherMonitorReader.Models
{
    public class WeatherMonitor
    {
        public Guid Id { get; set; }
        public string DegreeUnit { get; set; } = "C";

        public string PressureUnit { get; set; } = "hPa";

        public string SerialNumber { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string Firmware {  get; set; } = string.Empty;

        public string Language {  get; set; } = string.Empty;

        public string PressureType {  get; set; } = string.Empty;
        public string R { get; set; } = string.Empty;
        public string Bip { get; set; } = string.Empty;

        public List<WeatherMonitorSensor> Sensors { get; set; } = null!;
    }
}
