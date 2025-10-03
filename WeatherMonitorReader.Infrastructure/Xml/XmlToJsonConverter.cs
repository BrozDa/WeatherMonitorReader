
using Newtonsoft.Json;
using System.Xml;
using WeatherMonitorReader.Domain.Interfaces;

namespace WeatherMonitorReader.Infrastructure.Xml
{
    public class XmlToJsonConverter : IXmlToJsonConverter
    {
        public string ConvertXmlToJson(XmlDocument document)
        {
            return JsonConvert.SerializeXmlNode(document);
        }
    }
}
