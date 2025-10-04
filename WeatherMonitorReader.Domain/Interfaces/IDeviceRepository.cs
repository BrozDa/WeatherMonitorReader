using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Domain.Interfaces
{
    public interface IDeviceRepository
    {
        Task<WeatherMonitor?> GetBySerialNumber(string serialNumber);
        Task<WeatherMonitor> Add(WeatherMonitor monitor);


        Task<List<WeatherMonitorSensor>> GetSensors(WeatherMonitor monitor);
    }
}