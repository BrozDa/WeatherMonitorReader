using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WeatherMonitorReader.Infrastructure.Persistence
{
    public class WeatherMonitorContextFactory : IDesignTimeDbContextFactory<WeatherMonitorContext>
    {
        public WeatherMonitorContext CreateDbContext(string[] args)
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