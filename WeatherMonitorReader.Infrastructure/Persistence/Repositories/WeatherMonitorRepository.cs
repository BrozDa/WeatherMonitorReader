using Microsoft.EntityFrameworkCore;
using WeatherMonitorReader.Domain.Models;
using WeatherMonitorReader.Domain.Interfaces;

namespace WeatherMonitorReader.Infrastructure.Persistence.Repositories
{
    public class WeatherMonitorRepository(WeatherMonitorContext dbContext) : IWeatherMonitorRepository
    {
        public async Task<WeatherMonitor?> GetBySerialNumber(string serialNumber)
        {
            return await dbContext.WeatherMonitors.FirstOrDefaultAsync(wm => wm.SerialNumber == serialNumber);
        }
        public async Task<WeatherMonitor> AddMonitor(WeatherMonitor monitor) { 
            await dbContext.WeatherMonitors.AddAsync(monitor);
            await dbContext.SaveChangesAsync();
            return monitor;
        }
        public async Task<List<WeatherMonitorSensor>> GetSensors(WeatherMonitor monitor)
        {
            return await dbContext.WeatherMonitorSensors.Where(s => s.WeatherMonitorId == monitor.Id).ToListAsync();
        }

        public async Task<List<WeatherMonitorSensor>> AddSensors(List<WeatherMonitorSensor> sensors)
        {
            await dbContext.WeatherMonitorSensors.AddRangeAsync(sensors);
            await dbContext.SaveChangesAsync();

            return sensors;
        }
        public async Task<WeatherMonitorSnapshot> AddSnapshot(WeatherMonitorSnapshot snapshot)
        {
            await dbContext.WeatherMonitorSnapshots.AddAsync(snapshot);
            await dbContext.SaveChangesAsync();

            return snapshot;
        }
        public async Task<bool> AddSensorReadings(List<WeatherMonitorSensorReading> sensorReadings)
        {
            await dbContext.WeatherMonitorSensorReadings.AddRangeAsync(sensorReadings);
            await dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<bool> AddMinMaxReadings(List<WeatherMonitorSnapshotMinMax> minMaxReadings)
        {
            await dbContext.WeatherMonitorSnapshotMinMaxes.AddRangeAsync(minMaxReadings);
            await dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<bool> AddVariablesReadings(WeatherMonitorVariables variablesReading)
        {
            await dbContext.WeatherMonitorVariables.AddAsync(variablesReading);
            await dbContext.SaveChangesAsync();

            return true;
        }

        //create sensor readings
        //create minMaxes
        //create variables



    }
}
