using System.Xml;
using WeatherMonitorReader.Domain.Interfaces;

namespace WeatherMonitorReader.Infrastructure.Xml
{
    /// <summary>
    /// An Implementation of <see cref="IXmlFetcher"/> which fetches data from local file
    /// </summary>
    /// <param name="filePath">A <see cref="string"/> representation of path to the file</param>
    /// <remarks>Used only for local testing</remarks>
    public class XmlFromFileFetcher(string filePath) : IXmlFetcher
    {
        private readonly string _filePath = filePath;

        /// <inheritdoc/>
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
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}