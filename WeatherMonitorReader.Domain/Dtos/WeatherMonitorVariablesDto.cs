using System.Text.Json.Serialization;

namespace WeatherMonitorReader.Domain.Dtos
{
    public class WeatherMonitorVariablesDto
    {
        [JsonPropertyName("sunrise")]
        public string Sunrise { get; set; } = string.Empty;

        [JsonPropertyName("sunset")]
        public string Sunset { get; set; } = string.Empty;

        [JsonPropertyName("civstart")]
        public string Civstart { get; set; } = string.Empty;

        [JsonPropertyName("civend")]
        public string Civend { get; set; } = string.Empty;

        [JsonPropertyName("nautstart")]
        public string Nautstart { get; set; } = string.Empty;

        [JsonPropertyName("nautend")]
        public string Nautend { get; set; } = string.Empty;

        [JsonPropertyName("astrostart")]
        public string Astrostart { get; set; } = string.Empty;

        [JsonPropertyName("astroend")]
        public string Astroend { get; set; } = string.Empty;

        [JsonPropertyName("daylen")]
        public string Daylen { get; set; } = string.Empty;

        [JsonPropertyName("civlen")]
        public string Civlen { get; set; } = string.Empty;

        [JsonPropertyName("nautlen")]
        public string Nautlen { get; set; } = string.Empty;

        [JsonPropertyName("astrolen")]
        public string Astrolen { get; set; } = string.Empty;

        [JsonPropertyName("moonphase")]
        public string Moonphase { get; set; } = string.Empty;

        [JsonPropertyName("isday")]
        public string IsDay { get; set; } = string.Empty;

        [JsonPropertyName("bio")]
        public string Bio { get; set; } = string.Empty;

        [JsonPropertyName("pressure_old")]
        public string PressureOld { get; set; } = string.Empty;

        [JsonPropertyName("temperature_avg")]
        public string TemperatureAvg { get; set; } = string.Empty;

        [JsonPropertyName("agl")]
        public string Agl { get; set; } = string.Empty;

        [JsonPropertyName("fog")]
        public string Fog { get; set; } = string.Empty;

        [JsonPropertyName("lsp")]
        public string Lsp { get; set; } = string.Empty;
    }
}