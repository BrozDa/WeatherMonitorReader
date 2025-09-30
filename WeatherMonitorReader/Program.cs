using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherMonitorReader.Persistence;

namespace WeatherMonitorReader
{
    internal class Program
    {
        static void Main(string[] args)
        {

           

           IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();


            var connectionString = config.GetConnectionString("DefaultConnection");
            var optionBuilder = new DbContextOptionsBuilder<WeatherMonitorContext>()
                .UseSqlServer(connectionString);

            Console.WriteLine(connectionString);

            WeatherMonitorContext context = new WeatherMonitorContext(optionBuilder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Database.Migrate();
            
        }
    }
}
