using Newtonsoft.Json;
using System.Xml;
using WeatherMonitorReader.Domain.Interfaces;

namespace WeatherMonitorReader.Infrastructure.Xml
{
    /// <summary>
    /// An implementation of <see cref="IXmlToJsonConverter"/> used to convert <see cref="XmlDocument"/> to JSON in form of <see cref="string"/>
    /// </summary>
    public class XmlToJsonConverter : IXmlToJsonConverter
    {
        /// <inheritdoc/>
        public string ConvertXmlToJson(XmlDocument document)
        {
            return JsonConvert.SerializeXmlNode(document);
        }
    }
}