using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeatherMonitorReader.Application.Services;
using WeatherMonitorReader.Domain.Interfaces;
using WeatherMonitorReader.Infrastructure.Json;
using WeatherMonitorReader.Infrastructure.Persistence;
using WeatherMonitorReader.Infrastructure.Persistence.Repositories;
using WeatherMonitorReader.Infrastructure.Xml;

namespace WeatherMonitorReader.Console
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var hostBuilder = CreateBuilder();
            var host = hostBuilder.Build();

            await SetupDatabase(host);

            await host.RunAsync();
        }

        private static async Task SetupDatabase(IHost host)
        {
            using var services = host.Services.CreateScope();

            var context = services.ServiceProvider.GetService<WeatherMonitorContext>();

            await context!.Database.EnsureCreatedAsync();
        }
        public static IHostBuilder CreateBuilder()
        {
            /*
               https://manski.net/articles/csharp-dotnet/generichost
               https://jmezach.github.io/post/having-fun-with-the-dotnet-core-generic-host/
               https://sahansera.dev/dotnet-core-generic-host/
            */

            var builder = Host.CreateDefaultBuilder();

            builder.ConfigureAppConfiguration(cfg =>
            {
                cfg.SetBasePath(Directory.GetCurrentDirectory());
                cfg.AddJsonFile("appsettings.json");
                cfg.AddEnvironmentVariables();
                cfg.Build();
            });

            builder.ConfigureLogging((context,logging) =>
            {
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Information);
                logging.AddConfiguration(context.Configuration.GetSection("Logging"));
            });


            builder.ConfigureServices((context, services) =>
            {
                int interval = context.Configuration.GetValue<int>("ReaderSettings:Interval");
                string url = context.Configuration.GetValue<string>("ReaderSettings:Url")!;

                services.AddDbContext<WeatherMonitorContext>(options =>
                    options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddSingleton<IXmlFetcher, HttpXmlFetcher>(
                        fetcher => new HttpXmlFetcher(url));

                services.AddSingleton<IXmlToJsonConverter, XmlToJsonConverter>();
                services.AddSingleton<IJsonDeserializer, JsonDeserializer>();
                services.AddScoped<IWeatherMonitorRepository, WeatherMonitorRepository>();

                services.AddScoped<WeatherMonitorReadingService>();

                services.AddHostedService(provider =>
                    new BackgroundReadingService(
                        provider.GetRequiredService<WeatherMonitorReadingService>(),
                        provider.GetRequiredService<ILogger<BackgroundReadingService>>(),
                        interval));
            });

            return builder;
        }
    }
}