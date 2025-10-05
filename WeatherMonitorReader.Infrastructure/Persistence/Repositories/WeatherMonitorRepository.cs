using Microsoft.EntityFrameworkCore;
using WeatherMonitorReader.Domain.Interfaces;
using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// An implementation of <see cref="IWeatherMonitorRepository"/> which is interacting with the database
    /// using <see cref="WeatherMonitorContext"/>
    /// </summary>
    public class WeatherMonitorRepository(WeatherMonitorContext dbContext) : IWeatherMonitorRepository
    {
        /// <inheritdoc/>
        public async Task<WeatherMonitor?> GetBySerialNumber(string serialNumber)
        {
            return await dbContext.WeatherMonitors.FirstOrDefaultAsync(wm => wm.SerialNumber == serialNumber);
        }
        /// <inheritdoc/>
        public async Task<WeatherMonitor> AddMonitorAndSaveAsync(WeatherMonitor monitor)
        {
            await dbContext.WeatherMonitors.AddAsync(monitor);
            await dbContext.SaveChangesAsync();
            return monitor;
        }
        /// <inheritdoc/>
        public async Task<List<WeatherMonitorSensor>> GetSensors(WeatherMonitor monitor)
        {
            return await dbContext.WeatherMonitorSensors.Where(s => s.WeatherMonitorId == monitor.Id).ToListAsync();
        }
        /// <inheritdoc/>
        public async Task<List<WeatherMonitorSensor>> AddSensorsAndSaveAsync(List<WeatherMonitorSensor> sensors)
        {
            await dbContext.WeatherMonitorSensors.AddRangeAsync(sensors);
            await dbContext.SaveChangesAsync();

            return sensors;
        }
        /// <inheritdoc/>
        public async Task<WeatherMonitorSensor> AddSensorAndSaveAsync(WeatherMonitorSensor sensor)
        {
            await dbContext.WeatherMonitorSensors.AddAsync(sensor);
            await dbContext.SaveChangesAsync();

            return sensor;
        }
        /// <inheritdoc/>
        public async Task<WeatherMonitorSnapshot> AddSnapshotAndSaveAsync(WeatherMonitorSnapshot snapshot)
        {
            await dbContext.WeatherMonitorSnapshots.AddAsync(snapshot);
            await dbContext.SaveChangesAsync();

            return snapshot;
        }
        /// <inheritdoc/>
        public async Task AddSensorReadings(List<WeatherMonitorSensorReading> sensorReadings)
        {
            await dbContext.WeatherMonitorSensorReadings.AddRangeAsync(sensorReadings);
        }
        /// <inheritdoc/>
        public async Task AddMinMaxReadings(List<WeatherMonitorSnapshotMinMax> minMaxReadings)
        {
            await dbContext.WeatherMonitorSnapshotMinMaxes.AddRangeAsync(minMaxReadings);
        }
        /// <inheritdoc/>
        public async Task AddVariablesReadings(WeatherMonitorVariables variablesReading)
        {
            await dbContext.WeatherMonitorVariables.AddAsync(variablesReading);
        }
        /// <inheritdoc/>
        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

    }
}