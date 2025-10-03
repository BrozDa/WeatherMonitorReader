using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherMonitorReader.Domain.Dtos
{
    public class SensorTypeDto
    {
        [JsonPropertyName("sensor")]
        public List<WeatherMonitorSensorDto> Sensors { get; set; } = null!;
    }
}
