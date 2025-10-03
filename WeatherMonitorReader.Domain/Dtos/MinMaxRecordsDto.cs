using System.Text.Json.Serialization;

namespace WeatherMonitorReader.Domain.Dtos
{
    public class MinMaxRecordsDto
    {
        [JsonPropertyName("s")]
        public List<WeatherMonitorSnapshotMinMaxDto> MinMaxRecords { get; set; } = null!;
    }
}

