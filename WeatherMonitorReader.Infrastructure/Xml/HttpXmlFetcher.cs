using System.Xml;
using WeatherMonitorReader.Domain.Interfaces;

namespace WeatherMonitorReader.Infrastructure.Xml
{
    public class HttpXmlFetcher(string url) : IXmlFetcher
    {
        private readonly string _url = url;

        public async Task<XmlDocument?> FetchXmlDocumentAsync()
        {
            var responseString = await GetResponseString(_url);

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(responseString);

            return xmlDocument;
        }

        private async Task<string> GetResponseString(string url)
        {
            using HttpClient client = new HttpClient();

            using HttpResponseMessage response = await client.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }
    }
}