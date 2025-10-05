using Microsoft.EntityFrameworkCore;
using WeatherMonitorReader.Domain.Interfaces;
using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Infrastructure.Persistence.Repositories
{
    public class WeatherMonitorRepository(WeatherMonitorContext dbContext) : IWeatherMonitorRepository
    {
        public async Task<WeatherMonitor?> GetBySerialNumber(string serialNumber)
        {
            return await dbContext.WeatherMonitors.FirstOrDefaultAsync(wm => wm.SerialNumber == serialNumber);
        }

        public async Task<WeatherMonitor> AddMonitorAndSaveAsync(WeatherMonitor monitor)
        {
            await dbContext.WeatherMonitors.AddAsync(monitor);
            await dbContext.SaveChangesAsync();
            return monitor;
        }

        public async Task<List<WeatherMonitorSensor>> GetSensors(WeatherMonitor monitor)
        {
            return await dbContext.WeatherMonitorSensors.Where(s => s.WeatherMonitorId == monitor.Id).ToListAsync();
        }

        public async Task<List<WeatherMonitorSensor>> AddSensorsAndSaveAsync(List<WeatherMonitorSensor> sensors)
        {
            await dbContext.WeatherMonitorSensors.AddRangeAsync(sensors);
            await dbContext.SaveChangesAsync();

            return sensors;
        }

        public async Task<WeatherMonitorSensor> AddSensorAndSaveAsync(WeatherMonitorSensor sensor)
        {
            await dbContext.WeatherMonitorSensors.AddAsync(sensor);
            await dbContext.SaveChangesAsync();

            return sensor;
        }

        public async Task<WeatherMonitorSnapshot> AddSnapshotAndSaveAsync(WeatherMonitorSnapshot snapshot)
        {
            await dbContext.WeatherMonitorSnapshots.AddAsync(snapshot);
            await dbContext.SaveChangesAsync();

            return snapshot;
        }

        public async Task AddSensorReadings(List<WeatherMonitorSensorReading> sensorReadings)
        {
            await dbContext.WeatherMonitorSensorReadings.AddRangeAsync(sensorReadings);
        }

        public async Task AddMinMaxReadings(List<WeatherMonitorSnapshotMinMax> minMaxReadings)
        {
            await dbContext.WeatherMonitorSnapshotMinMaxes.AddRangeAsync(minMaxReadings);
        }

        public async Task AddVariablesReadings(WeatherMonitorVariables variablesReading)
        {
            await dbContext.WeatherMonitorVariables.AddAsync(variablesReading);
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        //create sensor readings
        //create minMaxes
        //create variables
    }
}