using System.Xml;

namespace WeatherMonitorReader.Domain.Interfaces
{
    /// <summary>
    /// Defines a contract for converting XML document to a string containing JSON
    /// </summary>
    public interface IXmlToJsonConverter
    {
        /// <summary>
        /// Converts Xmldocument to a string containing JSON
        /// </summary>
        /// <param name="document">A <see cref="XmlDocument"/> to be converted</param>
        /// <returns>A <see cref="string"/> containing data in JSON format</returns>
        string ConvertXmlToJson(XmlDocument document);
    }
}