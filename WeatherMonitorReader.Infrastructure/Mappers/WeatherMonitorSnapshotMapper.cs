using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Infrastructure.Mappers
{
    /// <summary>
    /// Provides functionality to map <see cref="WeatherMonitorDto"/> into <see cref="WeatherMonitorSnapshot"/>
    /// </summary>
    public class WeatherMonitorSnapshotMapper
    {
        /// <summary>
        /// Maps <see cref="WeatherMonitorDto"/> into <see cref="WeatherMonitorSnapshot"/>
        /// </summary>
        /// <param name="dto">A <see cref="WeatherMonitorDto"/> containing data used for mapping</param>
        /// <returns>A mapped <see cref="WeatherMonitorSnapshot"/></returns>
        public static WeatherMonitorSnapshot Map(WeatherMonitorDto dto)
        {
            var snapshot = new WeatherMonitorSnapshot();

            if (int.TryParse(dto.Runtime, out var runtime))
                snapshot.Runtime = runtime;

            if (int.TryParse(dto.Freemem, out var freemem))
                snapshot.Freemem = freemem;

            if (DateOnly.TryParse(dto.Date, out var date))
                snapshot.Date = date;

            if (TimeOnly.TryParse(dto.Time, out var time))
                snapshot.Time = time;

            return snapshot;
        }
    }
}