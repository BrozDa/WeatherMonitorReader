using WeatherMonitorReader.Domain.Models;
using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Enums;
using WeatherMonitorReader.Domain.Interfaces;
using WeatherMonitorReader.Infrastructure.Mappers;
namespace WeatherMonitorReader.Application.Services
{
    public class WeatherMonitorReadingService
    {
        private readonly IXmlFetcher _fetcher;
        private readonly IXmlToJsonConverter _converter;
        private readonly IWeatherMonitorRepository _repository;
        private readonly IJsonDeserializer _deserializer;

        private WeatherMonitor _monitor;
        private List<WeatherMonitorSensor> _monitorSensors;
        private WeatherMonitorSnapshot _snapShot;

        private bool _newMonitor = false;

        public WeatherMonitorReadingService(
            IXmlFetcher fetcher,
            IXmlToJsonConverter converter,
            IJsonDeserializer deserializer,
            IWeatherMonitorRepository deviceRepository)
        {
            _fetcher = fetcher;
            _converter = converter;
            _deserializer = deserializer;
            _repository = deviceRepository;

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
        }
        private async Task InitializeMonitor(WeatherMonitorDto monitorDto)
        {
            var entity = await _repository.GetBySerialNumber(monitorDto.SerialNumber);

            if(entity is null)
            {
                entity = await _repository
                    .AddMonitorAndSaveAsync(WeatherMonitorMapper.MapMonitor(monitorDto));

                _newMonitor = true;
            }

            _monitor = entity;
        }
        private async Task InitializeSensors(WeatherMonitorDto monitorDto)
        {
            _monitorSensors = _newMonitor 
                ? await _repository.GetSensors(_monitor)
                : await CreateAndStoreSensorsFromDto(monitorDto, _monitor.Id);
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
            var sensorReadings = CreateSensorReadings(reading.Device, _monitorSensors, _snapShot.Id);
            await _repository.AddSensorReadings(sensorReadings);

            //create minMaxes
            var minMaxReadings = CreateMinMaxReadings(reading.Device.MinMaxRecords, _monitorSensors, _snapShot.Id);
            await _repository.AddMinMaxReadings(minMaxReadings);

            //create variables
            var variables = WeatherMonitorVariableMapper.Map(reading.Device.Variables, _snapShot.Id);
            await _repository.AddVariablesReadings(variables);
        }
            
        private async Task<List<WeatherMonitorSensor>> CreateAndStoreSensorsFromDto(WeatherMonitorDto dto, Guid monitorId) {

            List<WeatherMonitorSensor> sensors = new();

            foreach (var sensorDto in dto.Input.Sensors) {
                sensors.Add(
                    WeatherMonitorSensorMapper
                    .MapSensor(sensorDto, SensorDirection.Input, monitorId));
            }
            foreach (var sensorDto in dto.Output.Sensors)
            {
                sensors.Add(
                    WeatherMonitorSensorMapper
                    .MapSensor(sensorDto, SensorDirection.Output, monitorId));
            }

            sensors = await _repository.AddSensorsAndSaveAsync(sensors);
            return sensors;

        }

        private List<WeatherMonitorSensorReading> CreateSensorReadings(WeatherMonitorDto dto, List<WeatherMonitorSensor> sensors, Guid snapShotId)
        {
            var sensorMap = sensors.ToDictionary(x => x.SensorId.ToString(), x => x);
            var sensorReadings = new List<WeatherMonitorSensorReading>();

            foreach (var sensorDto in dto.Input.Sensors)
            {
                sensorReadings.Add(
                    WeatherMonitorSensorReadingMapper
                    .Map(
                        sensorDto, 
                        sensorMap[sensorDto.Id].Id,
                        snapShotId
                    ));
            }
            foreach (var sensorDto in dto.Output.Sensors)
            {
                sensorReadings.Add(
                     WeatherMonitorSensorReadingMapper
                     .Map(
                         sensorDto,
                         sensorMap[sensorDto.Id].Id,
                         snapShotId
                     ));
            }

            return sensorReadings;


        }

        private List<WeatherMonitorSnapshotMinMax> CreateMinMaxReadings(MinMaxRecordsDto minMaxRecords, List<WeatherMonitorSensor> sensors, Guid snapShotId)
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
                         snapShotId
                     ));

            }

            return minMaxReadings;

        }

    }
}
