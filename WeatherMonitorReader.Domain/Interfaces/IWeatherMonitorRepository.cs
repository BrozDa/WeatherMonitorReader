using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Domain.Interfaces
{
    public interface IWeatherMonitorRepository
    {
        Task<WeatherMonitor?> GetBySerialNumber(string serialNumber);

        Task<WeatherMonitor> AddMonitorAndSaveAsync(WeatherMonitor monitor);

        Task<List<WeatherMonitorSensor>> GetSensors(WeatherMonitor monitor);

        Task<WeatherMonitorSensor> AddSensorAndSaveAsync(WeatherMonitorSensor sensor);

        Task<List<WeatherMonitorSensor>> AddSensorsAndSaveAsync(List<WeatherMonitorSensor> sensors);

        Task<WeatherMonitorSnapshot> AddSnapshotAndSaveAsync(WeatherMonitorSnapshot snapshot);

        Task AddSensorReadings(List<WeatherMonitorSensorReading> sensorReadings);

        Task AddMinMaxReadings(List<WeatherMonitorSnapshotMinMax> minMaxReadings);

        Task AddVariablesReadings(WeatherMonitorVariables variablesReading);

        Task SaveAsync();
    }
}