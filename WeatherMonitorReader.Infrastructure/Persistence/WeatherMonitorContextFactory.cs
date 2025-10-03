using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WeatherMonitorReader.Infrastructure.Persistence
{
    public class WeatherMonitorContextFactory
    {
        public WeatherMonitorContext CreateDbContext()
        {

            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var builder = new DbContextOptionsBuilder<WeatherMonitorContext>()
                .UseSqlServer(connectionString);

            return new WeatherMonitorContext(builder.Options);
        }
    }
}