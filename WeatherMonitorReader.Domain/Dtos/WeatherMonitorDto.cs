using System.Text.Json.Serialization;

namespace WeatherMonitorReader.Domain.Dtos
{
    public class WeatherMonitorDto
    {
        [JsonPropertyName("@degree")]
        public string DegreeUnit { get; set; } = string.Empty;

        [JsonPropertyName("@pressure")]
        public string PressureUnit { get; set; } = string.Empty;

        [JsonPropertyName("@serial_number")]
        public string SerialNumber { get; set; } = string.Empty;

        [JsonPropertyName("@model")]
        public string Model { get; set; } = string.Empty;

        [JsonPropertyName("@firmware")]
        public string Firmware { get; set; } = string.Empty;

        [JsonPropertyName("@runtime")]
        public string Runtime { get; set; } = string.Empty;

        [JsonPropertyName("@freemem")]
        public string Freemem { get; set; } = string.Empty;

        [JsonPropertyName("@date")]
        public string Date { get; set; } = string.Empty;

        [JsonPropertyName("@time")]
        public string Time { get; set; } = string.Empty;

        [JsonPropertyName("@language")]
        public string Language { get; set; } = string.Empty;

        [JsonPropertyName("@pressure_type")]
        public string PressureType { get; set; } = string.Empty;

        [JsonPropertyName("@r")]
        public string RFlag { get; set; } = string.Empty;

        [JsonPropertyName("@bip")]
        public string BipFlag { get; set; } = string.Empty;

        [JsonPropertyName("input")]
        public SensorTypeDto Input { get; set; } = null!;

        [JsonPropertyName("output")]
        public SensorTypeDto Output { get; set; } = null!;

        [JsonPropertyName("variable")]
        public WeatherMonitorVariablesDto Variables { get; set; } = null!;

        [JsonPropertyName("minmax")]
        public MinMaxRecordsDto MinMaxRecords { get; set; } = null!;

    }
}
