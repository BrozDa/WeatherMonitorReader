using System.Xml;

namespace WeatherMonitorReader.Domain.Interfaces
{
    public interface IXmlFetcher
    {
        Task<XmlDocument?> FetchXmlDocumentAsync();
    }
}