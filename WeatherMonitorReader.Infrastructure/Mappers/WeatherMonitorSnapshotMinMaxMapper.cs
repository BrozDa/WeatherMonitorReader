using WeatherMonitorReader.Domain.Models;
using WeatherMonitorReader.Domain.Dtos;

namespace WeatherMonitorReader.Infrastructure.Mappers
{
    public static class WeatherMonitorSnapshotMinMaxMapper
    {

        public static WeatherMonitorSnapshotMinMax Map(WeatherMonitorSnapshotMinMaxDto dto, Guid sensorId, Guid snapShotId)
        {
            var minMax = new WeatherMonitorSnapshotMinMax();

            if(double.TryParse(dto.Min, out var min))
                minMax.Min = min;
            
            if(double.TryParse(dto.Max, out var max))
                minMax.Max = max;

            minMax.SensorId = sensorId;
            minMax.SnapshotId = snapShotId;

            return minMax;
        }

    }
}
