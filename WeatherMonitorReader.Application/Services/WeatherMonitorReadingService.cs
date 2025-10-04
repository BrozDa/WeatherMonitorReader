using WeatherMonitorReader.Domain.Models;
using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Enums;
using WeatherMonitorReader.Domain.Interfaces;
using WeatherMonitorReader.Infrastructure.Mappers;
using Microsoft.Extensions.Logging;
using System.Threading;
namespace WeatherMonitorReader.Application.Services
{
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
        public async Task ProcessAsync()
        {
            var xml = await _fetcher.FetchXmlDocumentAsync();
            var jsonString = _converter.ConvertXmlToJson(xml);
            var xmlRootDto = _deserializer.Deserialize(jsonString);

            await InitializeMonitor(xmlRootDto.Device);
            await InitializeSensors(xmlRootDto.Device);
            await InitializeSnapshot(xmlRootDto.Device);

            await MapAndStoreReadings(xmlRootDto);

            _logger.LogInformation("[WeatherMonitorReadingService] Reading successful");
        }
        private async Task InitializeMonitor(WeatherMonitorDto monitorDto)
        {
            var entity = await _repository.GetBySerialNumber(monitorDto.SerialNumber);

            if (entity is null)
            {
                entity = await _repository
                    .AddMonitorAndSaveAsync(WeatherMonitorMapper.MapMonitor(monitorDto));

                _logger.LogInformation("[WeatherMonitorReadingService] Created new monitor; Monitor ID: {id}", entity.Id);

                _newMonitor = true;
            }
            else if (entity.Firmware != monitorDto.Firmware) {

                entity.Firmware = monitorDto.Firmware;
                await _repository.SaveAsync();
                _logger.LogInformation("[WeatherMonitorReadingService] Update Firmware value for monitor ID: {id}", entity.Id);
            }

            _monitor = entity;
        }
        private async Task InitializeSensors(WeatherMonitorDto monitorDto)
        {
            _monitorSensors = _newMonitor 
                ? await _repository.GetSensors(_monitor)
                : await CreateAndStoreSensorsFromDto(monitorDto);
        }
        private async Task InitializeSnapshot(WeatherMonitorDto monitorDto)
        {
            var snapShot = WeatherMonitorSnapshotMapper.MapSnapShot(monitorDto);

            snapShot.WeatherMonitor = _monitor;
            snapShot.WeatherMonitorId = _monitor.Id;

            _snapShot = await _repository.AddSnapshotAndSaveAsync(snapShot);
        }
        private async Task MapAndStoreReadings(XmlRootDto reading)
        {
            //create sensor readings
            var sensorReadings = await CreateSensorReadings(reading.Device, _monitorSensors);
            await _repository.AddSensorReadings(sensorReadings);

            //create minMaxes
            var minMaxReadings = CreateMinMaxReadings(reading.Device.MinMaxRecords, _monitorSensors);
            await _repository.AddMinMaxReadings(minMaxReadings);

            //create variables
            var variables = WeatherMonitorVariableMapper.Map(reading.Device.Variables, _snapShot.Id);
            await _repository.AddVariablesReadings(variables);
        }
        private async Task<List<WeatherMonitorSensor>> CreateAndStoreSensorsFromDto(WeatherMonitorDto dto) {

            List<WeatherMonitorSensor> sensors = new();

            foreach (var sensorDto in dto.Input.Sensors) {
                sensors.Add(
                    WeatherMonitorSensorMapper
                    .MapSensor(sensorDto, SensorDirection.Input, _monitor.Id));
            }
            foreach (var sensorDto in dto.Output.Sensors)
            {
                sensors.Add(
                    WeatherMonitorSensorMapper
                    .MapSensor(sensorDto, SensorDirection.Output, _monitor.Id));
            }

            sensors = await _repository.AddSensorsAndSaveAsync(sensors);
            _logger.LogInformation("[WeatherMonitorReadingService] Created new sensors for monitor ID: {id}", _monitor.Id);
            return sensors;

        }
        private async Task<WeatherMonitorSensor> CreateSingleSensor(WeatherMonitorSensorDto dto, SensorDirection direction) {

            var sensor = WeatherMonitorSensorMapper.MapSensor(dto,direction,_monitor.Id);
            sensor = await _repository.AddSensorAndSaveAsync(sensor);

            _logger.LogInformation("[WeatherMonitorReadingService] Created new sensor for existing monitor; " +
                "monitor ID: {monitorId}, Sensor Id: {sensorId}", _monitor.Id, sensor.Id);

            return sensor;
        }
        private async Task<List<WeatherMonitorSensorReading>> CreateSensorReadings(
            WeatherMonitorDto dto, 
            List<WeatherMonitorSensor> sensors)
        {
            var sensorIdMap = sensors.ToDictionary(x => x.SensorId.ToString(), x => x.Id);
            var sensorReadings = new List<WeatherMonitorSensorReading>();

            foreach (var sensorDto in dto.Input.Sensors)
            {

                sensorReadings.Add(
                    await CreatedSensorReading(sensorIdMap, sensorDto, SensorDirection.Input));
                
            }
            foreach (var sensorDto in dto.Output.Sensors)
            {
                sensorReadings.Add(
                    await CreatedSensorReading(sensorIdMap, sensorDto, SensorDirection.Output));
            }

            return sensorReadings;


        }
        private async Task<WeatherMonitorSensorReading> CreatedSensorReading(
            Dictionary<string, Guid> sensorIdMap, 
            WeatherMonitorSensorDto sensorDto, 
            SensorDirection sensorDirection)
        {

            var reading = new WeatherMonitorSensorReading();

            if (!sensorIdMap.TryGetValue(sensorDto.Id, out var _))
            {

                var newSensor = await CreateSingleSensor(sensorDto, SensorDirection.Output);

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
        private List<WeatherMonitorSnapshotMinMax> CreateMinMaxReadings(MinMaxRecordsDto minMaxRecords, List<WeatherMonitorSensor> sensors)
        {
            var sensorMap = sensors.ToDictionary(x => x.SensorId.ToString(), x => x);
            var minMaxReadings = new List<WeatherMonitorSnapshotMinMax>();

            foreach(var minMaxDto in minMaxRecords.MinMaxRecords)
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
