using WeatherMonitorReader.Domain.Models;
using WeatherMonitorReader.Domain.Dtos;

namespace WeatherMonitorReader.Infrastructure.Mappers
{
    internal static class WeatherMonitorSnapshotMinMaxMapper
    {

        public static WeatherMonitorSnapshotMinMax MapMinMax(WeatherMonitorSnapshotMinMaxDto dto, Guid SensorId)
        {
            var minMax = new WeatherMonitorSnapshotMinMax();

            if(double.TryParse(dto.Min, out var min))
                minMax.Min = min;
            
            if(double.TryParse(dto.Max, out var max))
                minMax.Max = max;

            minMax.SensorId = SensorId;

            return minMax;
        }

    }
}
