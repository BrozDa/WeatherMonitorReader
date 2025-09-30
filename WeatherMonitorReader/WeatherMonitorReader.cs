using Microsoft.Extensions.Configuration;
using WeatherMonitorReader.Persistence;

namespace WeatherMonitorReader
{
    internal class WeatherMonitorReader(IConfiguration config)
    {
        IConfiguration configuration = config;


    }
}
