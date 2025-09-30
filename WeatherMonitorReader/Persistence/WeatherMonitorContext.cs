using Microsoft.EntityFrameworkCore;
using WeatherMonitorReader.Models;

namespace WeatherMonitorReader.Persistence
{
    internal class WeatherMonitorContext(DbContextOptions options) :DbContext(options)
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
                .HasMany<WeatherMonitorSensor>()
                .WithOne(s => s.WeatherMonitor)
                .HasForeignKey(fk => fk.WeatherMonitorId);

            //1-to-m between Monitor and SnapShots
            modelBuilder.Entity<WeatherMonitor>()
                .HasMany<WeatherMonitorSnapshot>()
                .WithOne(ss => ss.WeatherMonitor)
                .HasForeignKey(fk => fk.WeatherMonitorId);

            //1-to-m between Sensor and SensorReading
            modelBuilder.Entity<WeatherMonitorSensor>()
                .HasMany<WeatherMonitorSensorReading>()
                .WithOne(sr => sr.Sensor)
                .HasForeignKey(fk => fk.SensorId);

            //1-to-m between Sensor and MinMax
            modelBuilder.Entity<WeatherMonitorSensor>()
                .HasMany<WeatherMonitorSnapshotMinMax>()
                .WithOne(ss => ss.Sensor)
                .HasForeignKey(fk => fk.SensorId);

            //1-to-m between Snapshot and SensorReading
            modelBuilder.Entity<WeatherMonitorSnapshot>()
                .HasMany<WeatherMonitorSensorReading>()
                .WithOne(sr => sr.Snapshot)
                .HasForeignKey(fk => fk.SnapshotId);

            //1-to-m between Snapshot and MinMax
            modelBuilder.Entity<WeatherMonitorSnapshot>()
                .HasMany<WeatherMonitorSnapshotMinMax>()
                .WithOne(ss => ss.Snapshot)
                .HasForeignKey(fk => fk.SnapshotId);

            //1-to-1 Snapshot and Variables
            modelBuilder.Entity<WeatherMonitorSnapshot>()
                .HasOne<WeatherMonitorVariables>()
                .WithOne(v => v.WeatherMonitorSnapshot)
                .HasForeignKey<WeatherMonitorVariables>(fk => fk.WeatherMonitorSnapshotId);


                


        }
    }
}
