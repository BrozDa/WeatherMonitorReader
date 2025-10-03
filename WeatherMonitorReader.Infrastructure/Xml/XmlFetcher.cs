using System.Xml;
using WeatherMonitorReader.Domain.Interfaces;
namespace WeatherMonitorReader.Infrastructure.Xml
{
    public class XmlFetcher() : IXmlFetcher
    {

        public async Task<XmlDocument> FetchXmlDocumentAsync()
        {
            using StreamReader reader = new StreamReader("E:\\Git Repos\\ItixoAssigment\\WeatherMonitorReader\\WeatherMonitorReader\\Input.xml");

            var xml = await reader.ReadToEndAsync();

            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);

            return document;
        }
    }
}
