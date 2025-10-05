using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Infrastructure.Mappers
{
    /// <summary>
    /// Provides functionality to map <see cref="WeatherMonitorVariablesDto"/> into <see cref="WeatherMonitorVariables"/>
    /// </summary>
    public static class WeatherMonitorVariableMapper
    {
        /// <summary>
        /// Maps <see cref="WeatherMonitorVariablesDto"/> into <see cref="WeatherMonitorVariables"/>
        /// </summary>
        /// <param name="dto">A <see cref="WeatherMonitorVariablesDto"/> containing data used for mapping</param>
        /// <param name="snapshotId">A <see cref="Guid"/> defining <see cref="WeatherMonitorSnapshot"/> to which variable model is associated to</param>
        /// <returns>A mapped <see cref="WeatherMonitorVariables"/></returns>
        public static WeatherMonitorVariables Map(WeatherMonitorVariablesDto dto, Guid snapshotId)
        {
            var variables = new WeatherMonitorVariables();

            if (TimeOnly.TryParse(dto.Sunrise, out TimeOnly sunrise))
                variables.Sunrise = sunrise;

            if (TimeOnly.TryParse(dto.Sunset, out TimeOnly sunset))
                variables.Sunrise = sunset;

            if (TimeOnly.TryParse(dto.Civstart, out TimeOnly civstart))
                variables.Civstart = civstart;

            if (TimeOnly.TryParse(dto.Civend, out TimeOnly civend))
                variables.Civend = civend;

            if (TimeOnly.TryParse(dto.Nautstart, out TimeOnly nautstart))
                variables.Nautstart = nautstart;

            if (TimeOnly.TryParse(dto.Nautend, out TimeOnly nautend))
                variables.Nautend = nautend;

            if (TimeOnly.TryParse(dto.Astrostart, out TimeOnly astrostart))
                variables.Astrostart = astrostart;

            if (TimeOnly.TryParse(dto.Astroend, out TimeOnly astroend))
                variables.Astroend = astroend;

            if (TimeOnly.TryParse(dto.Daylen, out TimeOnly daylen))
                variables.Daylen = daylen;

            if (TimeOnly.TryParse(dto.Civlen, out TimeOnly civlen))
                variables.Civlen = civlen;

            if (TimeOnly.TryParse(dto.Nautlen, out TimeOnly nautlen))
                variables.Nautlen = nautlen;

            if (TimeOnly.TryParse(dto.Astrolen, out TimeOnly astrolen))
                variables.Astrolen = astrolen;

            if (int.TryParse(dto.Moonphase, out int moonPhase))
                variables.Moonphase = moonPhase;

            if (int.TryParse(dto.IsDay, out int isDay))
                variables.IsDay = isDay == 1 ? true : false;

            if (int.TryParse(dto.Bio, out int bio))
                variables.Bio = bio;

            if (double.TryParse(dto.PressureOld, out double pressureOld))
                variables.PressureOld = pressureOld;

            if (double.TryParse(dto.TemperatureAvg, out double temperatureAvg))
                variables.TemperatureAvg = temperatureAvg;

            if (int.TryParse(dto.Agl, out int agl))
                variables.Agl = agl;

            if (int.TryParse(dto.Fog, out int fog))
                variables.Fog = fog;

            if (int.TryParse(dto.Lsp, out int lsp))
                variables.Lsp = lsp;

            variables.WeatherMonitorSnapshotId = snapshotId;

            return variables;
        }
    }
}