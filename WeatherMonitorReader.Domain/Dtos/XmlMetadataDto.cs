using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherMonitorReader.Domain.Dtos
{
    public class XmlMetadataDto
    {
        [JsonPropertyName("@version")]
        public string Version { get; set; } = string.Empty;
        [JsonPropertyName("@encoding")]
        public string Encoding { get; set; } = string.Empty;
    }
}
