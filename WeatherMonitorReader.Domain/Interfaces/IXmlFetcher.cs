using System.Xml;

namespace WeatherMonitorReader.Domain.Interfaces
{
    /// <summary>
    /// Represents contract for fetching XmlDocument
    /// </summary>
    public interface IXmlFetcher
    {
        /// <summary>
        /// Fetches XmlData
        /// </summary>
        /// <returns>A retrieved <see cref="XmlDocument"/> if action successfull, null otherwise</returns>
        Task<XmlDocument?> FetchXmlDocumentAsync();
    }
}