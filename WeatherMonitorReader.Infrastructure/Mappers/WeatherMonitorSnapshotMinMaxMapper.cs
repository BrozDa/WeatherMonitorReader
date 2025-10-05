using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Infrastructure.Mappers
{
    /// <summary>
    /// Provides functionality to map <see cref="WeatherMonitorSnapshotMinMaxDto"/> into <see cref="WeatherMonitorSnapshotMinMax"/>
    /// </summary>
    public static class WeatherMonitorSnapshotMinMaxMapper
    {
        /// <summary>
        /// Maps <see cref="WeatherMonitorSnapshotMinMaxDto"/> into <see cref="WeatherMonitorSnapshotMinMax"/>
        /// </summary>
        /// <param name="dto">A <see cref="WeatherMonitorSnapshotMinMaxDto"/> containing data used for mapping</param>
        /// <param name="sensorId">A <see cref="Guid"/> defining <see cref="WeatherMonitorSensor"/> to which minMaxreading is associated to</param>
        /// <param name="snapshotId">A <see cref="Guid"/> defining <see cref="WeatherMonitorSnapshot"/> to which minMaxreading is associated to</param>
        /// <returns>A mapped <see cref="WeatherMonitorSnapshotMinMax"/></returns>
        public static WeatherMonitorSnapshotMinMax Map(WeatherMonitorSnapshotMinMaxDto dto, Guid sensorId, Guid snapshotId)
        {
            var minMax = new WeatherMonitorSnapshotMinMax();

            if (double.TryParse(dto.Min, out var min))
                minMax.Min = min;

            if (double.TryParse(dto.Max, out var max))
                minMax.Max = max;

            minMax.SensorId = sensorId;
            minMax.SnapshotId = snapshotId;

            return minMax;
        }
    }
}