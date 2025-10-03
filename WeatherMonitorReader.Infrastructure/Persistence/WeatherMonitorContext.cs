using Microsoft.EntityFrameworkCore;
using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Infrastructure.Persistence
{
    public class WeatherMonitorContext(DbContextOptions<WeatherMonitorContext> options) : DbContext(options)
    {
        public DbSet<WeatherMonitor> WeatherMonitors { get; set; } = null!;
        public DbSet<WeatherMonitorSensor> WeatherMonitorSensors { get; set; } = null!;
        public DbSet<WeatherMonitorSnapshot> WeatherMonitorSnapshots { get; set; } = null!;
        public DbSet<WeatherMonitorSnapshotMinMax> WeatherMonitorSnapshotMinMaxes { get; set; } = null!;
        public DbSet<WeatherMonitorVariables> WeatherMonitorVariables { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //1-to-m between Monitor and Sensors
            modelBuilder.Entity<WeatherMonitor>()
                .HasMany(m => m.Sensors)
                .WithOne(s => s.WeatherMonitor)
                .HasForeignKey(fk => fk.WeatherMonitorId);

            //1-to-m between Monitor and SnapShots
            modelBuilder.Entity<WeatherMonitor>()
                .HasMany(m => m.Snapshots)
                .WithOne(ss => ss.WeatherMonitor)
                .HasForeignKey(fk => fk.WeatherMonitorId);

            //1-to-m between Sensor and SensorReading
            modelBuilder.Entity<WeatherMonitorSensor>()
                .HasMany(s => s.SensorReadings)
                .WithOne(sr => sr.Sensor)
                .HasForeignKey(fk => fk.SensorId)
                .OnDelete(DeleteBehavior.NoAction);

            //1-to-m between Sensor and MinMax
            modelBuilder.Entity<WeatherMonitorSensor>()
                .HasMany(s => s.SensorMinMaxes)
                .WithOne(mm => mm.Sensor)
                .HasForeignKey(fk => fk.SensorId)
                .OnDelete(DeleteBehavior.NoAction);

            //1-to-m between Snapshot and SensorReading
            modelBuilder.Entity<WeatherMonitorSnapshot>()
                .HasMany(ss => ss.SensorReadings)
                .WithOne(sr => sr.Snapshot)
                .HasForeignKey(fk => fk.SnapshotId)
                .OnDelete(DeleteBehavior.NoAction);

            //1-to-m between Snapshot and MinMax
            modelBuilder.Entity<WeatherMonitorSnapshot>()
                .HasMany(ss => ss.SensorMinMaxes)
                .WithOne(mm => mm.Snapshot)
                .HasForeignKey(fk => fk.SnapshotId)
                .OnDelete(DeleteBehavior.NoAction);

            //1-to-1 Snapshot and Variables
            modelBuilder.Entity<WeatherMonitorSnapshot>()
                .HasOne(ss => ss.WeatherMonitorVariables)
                .WithOne(v => v.WeatherMonitorSnapshot)
                .HasForeignKey<WeatherMonitorVariables>(fk => fk.WeatherMonitorSnapshotId);

        }
    }
}
