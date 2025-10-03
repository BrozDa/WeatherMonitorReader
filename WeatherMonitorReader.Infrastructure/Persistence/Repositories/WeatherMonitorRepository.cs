using Microsoft.EntityFrameworkCore;
using WeatherMonitorReader.Domain.Models;
using WeatherMonitorReader.Domain.Interfaces;

namespace WeatherMonitorReader.Infrastructure.Persistence.Repositories
{
    public class WeatherMonitorRepository(WeatherMonitorContext dbContext) : IDeviceRepository
    {
        public async Task<WeatherMonitor?> GetBySerialNumber(string serialNumber)
        {
            return await dbContext.WeatherMonitors.FirstOrDefaultAsync(wm => wm.SerialNumber == serialNumber);
        }
        public async Task<WeatherMonitor> Add(WeatherMonitor monitor) { 
            await dbContext.WeatherMonitors.AddAsync(monitor);
            await dbContext.SaveChangesAsync();
            return monitor;
        }


    }
}
