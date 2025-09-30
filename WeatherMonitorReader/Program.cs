using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Xml;
using WeatherMonitorReader.Persistence;

namespace WeatherMonitorReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            string json = JsonConvert.SerializeXmlNode(document);

            Console.WriteLine(json);

        }
    }
}
