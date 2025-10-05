using WeatherMonitorReader.Domain.Models;

namespace WeatherMonitorReader.Domain.Interfaces
{
    /// <summary>
    /// Represents contract for storing and retrieving WeatherMonitor related objects 
    /// Such as: <see cref="WeatherMonitor"/>, 
    /// <see cref="WeatherMonitorSensor"/>, 
    /// <see cref="WeatherMonitorSnapshot"/>, 
    /// <see cref="WeatherMonitorSensorReading"/>,
    /// <see cref="WeatherMonitorSnapshotMinMax"/>, 
    /// <see cref="WeatherMonitorVariables"/>
    /// </summary>
    public interface IWeatherMonitorRepository
    {
        /// <summary>
        /// Retrieves existing monitor from repository based on its serial number
        /// </summary>
        /// <param name="serialNumber">A <see cref="string"/> containing unique serial number</param>
        /// <returns>An <see cref="WeatherMonitor"/> if found, <see cref="null"/> otherwise</returns>
        Task<WeatherMonitor?> GetBySerialNumber(string serialNumber);

        /// <summary>
        /// Adds new monitor to the repository
        /// </summary>
        /// <param name="monitor">A <see cref="WeatherMonitor"/> to be added</param>
        /// <returns>The added <see cref="<see cref="WeatherMonitor"/> "/></returns>
        Task<WeatherMonitor> AddMonitorAndSaveAsync(WeatherMonitor monitor);

        /// <summary>
        /// Retrieves all sensor for passed monitor
        /// </summary>
        /// <param name="monitor">A <see cref="WeatherMonitor"/> whose sensors will be retrieved</param>
        /// <returns>A list of retrieved <see cref="WeatherMonitorSensor"/></returns>
        Task<List<WeatherMonitorSensor>> GetSensors(WeatherMonitor monitor);

        /// <summary>
        /// Adds new Sensor to the repository
        /// </summary>
        /// <param name="sensor">A <see cref="WeatherMonitorSensor"/> to be added</param>
        /// <returns>The added <see cref="WeatherMonitorSensor"/></returns>
        Task<WeatherMonitorSensor> AddSensorAndSaveAsync(WeatherMonitorSensor sensor);

        /// <summary>
        /// Adds List of sensors to the repository
        /// </summary>
        /// <param name="sensor">A list of <see cref="WeatherMonitorSensor"/> to be added</param>
        /// <returns>The list of  added <see cref="WeatherMonitorSensor"/></returns>
        Task<List<WeatherMonitorSensor>> AddSensorsAndSaveAsync(List<WeatherMonitorSensor> sensors);

        /// <summary>
        /// Adds new snapshot to the repository
        /// </summary>
        /// <param name="snapshot">A <see cref="WeatherMonitorSnapshot"/> to be added</param>
        /// <returns>The added <see cref="WeatherMonitorSnapshot"/></returns>
        Task<WeatherMonitorSnapshot> AddSnapshotAndSaveAsync(WeatherMonitorSnapshot snapshot);

        /// <summary>
        /// Adds list of sensor readings to the repository
        /// </summary>
        /// <param name="sensorReadings">A list of <see cref="WeatherMonitorSensorReading"/> to be added</param>
        Task AddSensorReadings(List<WeatherMonitorSensorReading> sensorReadings);

        /// <summary>
        /// Adds list of min max values to the repository
        /// </summary>
        /// <param name="minMaxReadings">A list of <see cref="WeatherMonitorSnapshotMinMax"/> to be added</param>
        Task AddMinMaxReadings(List<WeatherMonitorSnapshotMinMax> minMaxReadings);

        /// <summary>
        /// Adds variables to the repository
        /// </summary>
        /// <param name="variablesReading">A <see cref="WeatherMonitorVariables"/> to be added</param>
        Task AddVariablesReadings(WeatherMonitorVariables variablesReading);

        /// <summary>
        /// Persists any pending changes to the data store.
        /// </summary>
        Task SaveAsync();
    }
}