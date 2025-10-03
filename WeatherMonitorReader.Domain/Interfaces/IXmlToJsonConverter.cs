using System.Xml;

namespace WeatherMonitorReader.Domain.Interfaces
{
    public interface IXmlToJsonConverter
    {
        string ConvertXmlToJson(XmlDocument document);
    }
}
