using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Xml;
using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Enums;
using WeatherMonitorReader.Domain.Interfaces;
using WeatherMonitorReader.Domain.Models;
using WeatherMonitorReader.Infrastructure.Mappers;

namespace WeatherMonitorReader.Application.Services
{
    /// <summary>
    /// A service which pulls & process data from WeatherMonitor
    /// </summary>
    public class WeatherMonitorReadingService
    {
        private readonly IXmlFetcher _fetcher;
        private readonly IXmlToJsonConverter _converter;
        private readonly IJsonDeserializer _deserializer;
        private readonly IWeatherMonitorRepository _repository;
        private readonly ILogger<WeatherMonitorReadingService> _logger;


        private WeatherMonitor _monitor;
        private List<WeatherMonitorSensor> _monitorSensors;
        private WeatherMonitorSnapshot _snapShot;

        private WeatherMonitor? _lastUsedMonitor = null;

        private bool _newMonitor = false;

        public WeatherMonitorReadingService(
            IXmlFetcher fetcher,
            IXmlToJsonConverter converter,
            IJsonDeserializer deserializer,
            IWeatherMonitorRepository deviceRepository,
            ILogger<WeatherMonitorReadingService> logger)
        {
            _fetcher = fetcher;
            _converter = converter;
            _deserializer = deserializer;
            _repository = deviceRepository;
            _logger = logger;
            _monitor = new();
            _monitorSensors = new();
            _snapShot = new();
        }

        /// <summary>
        /// Pulls data and calls internal methods to process pulled data
        /// </summary>
        public async Task ProcessAsync()
        {
            try
            {
                var xml = await _fetcher.FetchXmlDocumentAsync();

                var jsonString = _converter.ConvertXmlToJson(xml!);
                var xmlRootDto = _deserializer.Deserialize(jsonString);

                await InitializeMonitorAsync(xmlRootDto.Device);
                await InitializeSensorsAsync(xmlRootDto.Device);
                await InitializeSnapshotAsync(xmlRootDto.Device);

                await MapAndStoreReadingsAsync(xmlRootDto);

                _lastUsedMonitor = _monitor;
            }
            catch (XmlException)
            {
                _logger.LogWarning("[WeatherMonitorReadingService][IXmlFetcher] The provided request response was not in valid XML format");
                return;
            }
            catch (UriFormatException)
            {
                _logger.LogWarning("[WeatherMonitorReadingService][IXmlFetcher] The provided request URI is not valid relative or absolute URI");
                return;
            }
            catch (HttpRequestException)
            {
                _logger.LogWarning("[WeatherMonitorReadingService][IXmlFetcher] The request failed due to an issue getting a valid HTTP response");

                if(_lastUsedMonitor is null)
                {
                    _logger.LogCritical("[WeatherMonitorReadingService] Monitor down - no previously used monitor to reference");
                }
                else
                {
                    _snapShot = WeatherMonitorSnapshot.MonitorDownSnapshot(_lastUsedMonitor!.Id);
                    await _repository.AddSnapshotAndSaveAsync(_snapShot);
                    _logger.LogCritical("[WeatherMonitorReadingService] Monitor down - used last used monitor for reference");
                }

                return;
            }
            catch (JsonException)
            {
                _logger.LogWarning("[WeatherMonitorReadingService][IJsonDeserializer] Deserialization failed");
                return;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("[WeatherMonitorReadingService] Unhandled exception while processing the request {messsage}, {trace}", ex.Message, ex.StackTrace);
                return;
            }
        }
        /// <summary>
        /// Initializes WeatherMonitor field based on data in Dto
        /// </summary>
        /// <param name="monitorDto">A <see cref="WeatherMonitorDto"/> used to pull from DB/ initialize new weather monitor</param>
        /// <remarks>
        /// Checks whether DB contains monitor with same SN as DTO. If not, new monitor will be created and 
        /// internal field marking new monitor is set.
        /// In addition, double check if monitor firmware was not updated - any change is stored
        /// </remarks>
        private async Task InitializeMonitorAsync(WeatherMonitorDto monitorDto)
        {
            var entity = await _repository.GetBySerialNumber(monitorDto.SerialNumber);

            if (entity is null)
            {
                entity = await _repository
                    .AddMonitorAndSaveAsync(WeatherMonitorMapper.Map(monitorDto));

                _logger.LogInformation("[WeatherMonitorReadingService] Created new monitor; Monitor ID: {id}", entity.Id);

                _newMonitor = true;
            }
            else if (entity.Firmware != monitorDto.Firmware)
            {
                entity.Firmware = monitorDto.Firmware;
                await _repository.SaveAsync();
                _logger.LogInformation("[WeatherMonitorReadingService] Update Firmware value for monitor ID: {id}", entity.Id);
            }

            _monitor = entity;
        }

        /// <summary>
        /// Initializes WeatherMonitor sensors field based on data in Dto
        /// </summary>
        /// <param name="monitorDto">A <see cref="WeatherMonitorDto"/> used to pull from DB/ initialize new weather monitor sensors</param>
        /// <remarks>
        /// Checks whether new monitor was created in previous step. If so, sensors will be created from DTO. Sensors will be pulled from DB in case 
        /// of existing monitor
        /// </remarks>
        private async Task InitializeSensorsAsync(WeatherMonitorDto monitorDto)
        {
            if (_newMonitor)
            {
                _monitorSensors = await CreateAndStoreSensorsFromDtoAsync(monitorDto);
                _newMonitor = false;
            }
            else
            {
                _monitorSensors= await _repository.GetSensors(_monitor);
            }
        }
        /// <summary>
        /// Initializes new reading snapshot based on data in Dto and stores it to Db
        /// </summary>
        /// <param name="monitorDto">A <see cref="WeatherMonitorDto"/> used to initialize new weather monitor snapshot</param>
        private async Task InitializeSnapshotAsync(WeatherMonitorDto monitorDto)
        {
            var snapShot = WeatherMonitorSnapshotMapper.Map(monitorDto);

            snapShot.WeatherMonitor = _monitor;
            snapShot.WeatherMonitorId = _monitor.Id;

            _snapShot = await _repository.AddSnapshotAndSaveAsync(snapShot);
        }
        /// <summary>
        /// Maps and stores Sensor Readings, MinMax Readings and Variables
        /// </summary>
        /// <param name="reading">An <see cref="XmlRootDto"/> object container deserialized data</param>
        private async Task MapAndStoreReadingsAsync(XmlRootDto reading)
        {
            //create sensor readings
            var sensorReadings = await CreateSensorReadingsAsync(reading.Device);
            await _repository.AddSensorReadings(sensorReadings);
            await _repository.SaveAsync();

            //create minMaxes
            var minMaxReadings = CreateMinMaxReadings(reading.Device.MinMaxRecords);
            await _repository.AddMinMaxReadings(minMaxReadings);
            await _repository.SaveAsync();

            //create variables
            var variables = WeatherMonitorVariableMapper.Map(reading.Device.Variables, _snapShot.Id);
            await _repository.AddVariablesReadings(variables);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// Creates List of new sensors based on data in passed dto and stores it to the repository
        /// </summary>
        /// <param name="dto">A <see cref="WeatherMonitorDto"/> containing data to initialize sensors</param>
        /// <returns>A list of created <see cref="WeatherMonitorSensor"/></returns>
        private async Task<List<WeatherMonitorSensor>> CreateAndStoreSensorsFromDtoAsync(WeatherMonitorDto dto)
        {
            List<WeatherMonitorSensor> sensors = new();

            foreach (var sensorDto in dto.Input.Sensors)
            {
                sensors.Add(
                    WeatherMonitorSensorMapper
                    .Map(sensorDto, SensorDirection.Input, _monitor.Id));
            }
            foreach (var sensorDto in dto.Output.Sensors)
            {
                sensors.Add(
                    WeatherMonitorSensorMapper
                    .Map(sensorDto, SensorDirection.Output, _monitor.Id));
            }

            sensors = await _repository.AddSensorsAndSaveAsync(sensors);
            _logger.LogInformation("[WeatherMonitorReadingService] Created new sensors for monitor ID: {id}", _monitor.Id);
            return sensors;
        }
        /// <summary>
        /// Creates a new sensor based on data in passed dto and stores it to the repository
        /// </summary>
        /// <param name="dto">A <see cref="WeatherMonitorSensorDto"/> containing data to initialize sensor</param>
        /// <param name="direction">A <see cref="SensorDirection"/> telling the sensor direction</param>
        /// <returns></returns>
        private async Task<WeatherMonitorSensor> CreateSingleSensorAsync(WeatherMonitorSensorDto dto, SensorDirection direction)
        {
            var sensor = WeatherMonitorSensorMapper.Map(dto, direction, _monitor.Id);
            sensor = await _repository.AddSensorAndSaveAsync(sensor);

            _logger.LogInformation("[WeatherMonitorReadingService] Created new sensor for existing monitor; " +
                "monitor ID: {monitorId}, Sensor Id: {sensorId}", _monitor.Id, sensor.Id);

            return sensor;
        }
        /// <summary>
        /// Creates sensor readings for existing sensors based on data in passed dto
        /// </summary>
        /// <param name="dto">A <see cref="WeatherMonitorDto"/> containing data to initialize readings</param>
        /// <returns>A list of created <see cref="WeatherMonitorSensorReading"/></returns>
        /// <remarks>
        /// Utilizes a map of sensor Id from DTO (eg: 1002) to actual sensor Guid for quick lookup
        /// </remarks>
        private async Task<List<WeatherMonitorSensorReading>> CreateSensorReadingsAsync(
            WeatherMonitorDto dto)
        {
            var sensorIdMap = _monitorSensors.ToDictionary(x => x.SensorId.ToString(), x => x.Id);
            var sensorReadings = new List<WeatherMonitorSensorReading>();

            if(dto.Input?.Sensors is not null)
            {
                foreach (var sensorDto in dto.Input.Sensors)
                {
                    sensorReadings.Add(
                        await CreateSensorReadingAsync(sensorIdMap, sensorDto, SensorDirection.Input));
                }
            }
            else
            {
                _logger.LogWarning("[WeatherMonitorReadingService] Missing input sensors");
            }


            if (dto.Output?.Sensors is not null)
            {
                foreach (var sensorDto in dto.Output.Sensors)
                {
                    sensorReadings.Add(
                        await CreateSensorReadingAsync(sensorIdMap, sensorDto, SensorDirection.Output));
                }
            }
            else
            {
                _logger.LogWarning("[WeatherMonitorReadingService] Missing output sensors");
            }

            return sensorReadings;
        }
        /// <summary>
        /// Creates new sensor reading
        /// </summary>
        /// <param name="sensorIdMap">A Dictionary map of dto Ids to actual sensor Guids</param>
        /// <param name="sensorDto">A <see cref="WeatherMonitorSensorDto"/> containing new data</param>
        /// <param name="sensorDirection">A <see cref="SensorDirection"/> telling the sensor direction</param>
        /// <returns>Initialized <see cref="WeatherMonitorSensorReading"/></returns>
        /// <remarks>If there is new sensor which is not stored in the database then new sensor is created, stored to the repository</remarks>
        private async Task<WeatherMonitorSensorReading> CreateSensorReadingAsync(
            Dictionary<string, Guid> sensorIdMap,
            WeatherMonitorSensorDto sensorDto,
            SensorDirection sensorDirection)
        {
            var reading = new WeatherMonitorSensorReading();

            if (!sensorIdMap.TryGetValue(sensorDto.Id, out var _))
            {
                var newSensor = await CreateSingleSensorAsync(sensorDto, SensorDirection.Output);

                reading = WeatherMonitorSensorReadingMapper.Map(
                    sensorDto,
                    newSensor.Id,
                    _snapShot.Id
                 );
            }
            else
            {
                reading = WeatherMonitorSensorReadingMapper.Map(
                    sensorDto,
                    sensorIdMap[sensorDto.Id],
                    _snapShot.Id
                 );
            }
            return reading;
        }
        /// <summary>
        /// Creates a List of <see cref="WeatherMonitorSnapshotMinMax"/> based on data in passed dto 
        /// </summary>
        /// <param name="minMaxRecords">A <see cref="MinMaxRecordsDto"/> containing min max values </param>
        /// <returns> A List of <see cref="WeatherMonitorSnapshotMinMax"/></returns>
        /// <remarks>
        /// Utilizes a map of sensor Id from DTO (eg: 1002) to actual sensor Guid for quick lookup
        /// </remarks>
        private List<WeatherMonitorSnapshotMinMax> CreateMinMaxReadings(MinMaxRecordsDto minMaxRecords)
        {
            var sensorMap = _monitorSensors.ToDictionary(x => x.SensorId.ToString(), x => x);
            var minMaxReadings = new List<WeatherMonitorSnapshotMinMax>();

            foreach (var minMaxDto in minMaxRecords.MinMaxRecords)
            {
                minMaxReadings.Add(
                     WeatherMonitorSnapshotMinMaxMapper
                     .Map(
                         minMaxDto,
                         sensorMap[minMaxDto.SensorId].Id,
                         _snapShot.Id
                     ));
            }

            return minMaxReadings;
        }
    }
}