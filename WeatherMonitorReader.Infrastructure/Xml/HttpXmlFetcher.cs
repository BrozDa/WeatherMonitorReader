using System.Xml;
using WeatherMonitorReader.Domain.Interfaces;

namespace WeatherMonitorReader.Infrastructure.Xml
{
    /// <summary>
    /// An Implementation of <see cref="IXmlFetcher"/> which fetches data from remote resource using HTTP
    /// </summary>
    /// <param name="url">A <see cref="string"/> representation of url</param>
    public class HttpXmlFetcher(string url) : IXmlFetcher
    {
        private readonly string _url = url;

        /// <inheritdoc/>
        public async Task<XmlDocument?> FetchXmlDocumentAsync()
        {
            var responseString = await GetResponseString();

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(responseString);

            return xmlDocument;
        }

        /// <summary>
        /// Creates one use HTTP client which request resource from remote resource
        /// </summary>
        /// <returns>A Http response content in form of <see cref="string"/></returns>
        private async Task<string> GetResponseString()
        {
            using HttpClient client = new HttpClient();

            using HttpResponseMessage response = await client.GetAsync(_url);

            return await response.Content.ReadAsStringAsync();
        }
    }
}