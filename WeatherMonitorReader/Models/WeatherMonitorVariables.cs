namespace WeatherMonitorReader.Models
{
    public class WeatherMonitorVariables
    {
        public Guid Id { get; set; }
        public Guid WeatherMonitorSnapshotId { get; set; }
        public WeatherMonitorSnapshot WeatherMonitorSnapshot { get; set; } = null!;
        public TimeOnly Sunrise { get; set; }
        public TimeOnly Sunset { get; set; }
        public TimeOnly Civstart { get; set; }
        public TimeOnly Civend { get; set; }
        public TimeOnly Nautstart { get; set; }
        public TimeOnly Nautend { get; set; }
        public TimeOnly Astrostart { get; set; }
        public TimeOnly Astroend { get; set; }
        public TimeOnly Daylen { get; set; }
        public TimeOnly Civlen { get; set; }
        public TimeOnly Nautlen { get; set; }
        public TimeOnly Astrolen { get; set; }
        public int Moonphase { get; set; }
        public bool IsDay {  get; set; }
        public int Bio {  get; set; }
        public double PressureOld { get; set; }
        public double TemperatureAvg {  get; set; }
        public int Agl { get; set; }
        public int Fog { get; set; }
        public int Lsp { get; set; }    


    }
}
