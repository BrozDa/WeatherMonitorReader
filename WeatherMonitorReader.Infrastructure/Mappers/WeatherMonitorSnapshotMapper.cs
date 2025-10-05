using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Infrastructure.Mappers
{
    public class WeatherMonitorSnapshotMapper
    {
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