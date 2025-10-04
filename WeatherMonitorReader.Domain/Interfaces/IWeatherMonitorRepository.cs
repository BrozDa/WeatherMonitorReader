using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Domain.Interfaces
{
    public interface IWeatherMonitorRepository
    {
        Task<WeatherMonitor?> GetBySerialNumber(string serialNumber);
        Task<WeatherMonitor> AddMonitor(WeatherMonitor monitor);
        Task<List<WeatherMonitorSensor>> GetSensors(WeatherMonitor monitor);
        Task<List<WeatherMonitorSensor>> AddSensors(List<WeatherMonitorSensor> sensors);
        Task<WeatherMonitorSnapshot> AddSnapshot(WeatherMonitorSnapshot snapshot);
        Task<bool> AddSensorReadings(List<WeatherMonitorSensorReading> sensorReadings);
        Task<bool> AddMinMaxReadings(List<WeatherMonitorSnapshotMinMax> minMaxReadings);
        Task<bool> AddVariablesReadings(WeatherMonitorVariables variablesReading);
    }
}