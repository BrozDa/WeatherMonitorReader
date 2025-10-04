using System.Xml;
using WeatherMonitorReader.Domain.Interfaces;
namespace WeatherMonitorReader.Infrastructure.Xml
{
    public class XmlFromFileFetcher(string filePath) : IXmlFetcher
    {
        private readonly string _filePath = filePath;
        public async Task<XmlDocument?> FetchXmlDocumentAsync()
        {
            try
            {
                using StreamReader reader = new StreamReader(_filePath);

                var xml = await reader.ReadToEndAsync();

                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);

                return document;
            }
            catch (Exception ex) {
                return null;
            }
            
        }
    }
}
