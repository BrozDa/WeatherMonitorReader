using WeatherMonitorReader.Domain.Models;
using WeatherMonitorReader.Domain.Dtos;
using WeatherMonitorReader.Domain.Enums;
using WeatherMonitorReader.Domain.Interfaces;
using WeatherMonitorReader.Infrastructure.Mappers;
namespace WeatherMonitorReader.Application.Services
{
    public class WeatherMonitorReadingService(
        IXmlFetcher fetcher,
        IXmlToJsonConverter converter,
        IJsonDeserializer serializer,
        IWeatherMonitorRepository deviceRepository)
    {

        public async Task ProcessAsync()
        {
            var xml = await fetcher.FetchXmlDocumentAsync();
            var jsonString = converter.ConvertXmlToJson(xml);
            var xmlObject = serializer.Deserialize(jsonString);

            await StoreReadings(xmlObject);
        }

        private async Task StoreReadings(XmlRootDto reading)
        {
            var monitor = await GetMonitorEntity(reading.Device);

            var sensorList = new List<WeatherMonitorSensor>();
            var snapShot = new WeatherMonitorSnapshot();
            var sensorReadings = new List<WeatherMonitorSensorReading>();
            var minMaxReadings = new List<WeatherMonitorSnapshotMinMax>();
            var variables = new WeatherMonitorVariables();

            if (monitor is null) //new monitor -> create monitor and sensors
            {
                monitor = await deviceRepository
                    .AddMonitor(WeatherMonitorMapper.MapMonitor(reading.Device));

                sensorList = await CreateSensorsFromDto(reading.Device, monitor.Id);
            }
            else //existing - retrieve list
            {
                sensorList = await GetSensorEntities(monitor);
            }

            //create snapshot
            snapShot = await CreateSnapshot(reading.Device, monitor);

            //create sensor readings
            sensorReadings = CreateSensorReadings(reading.Device, sensorList,snapShot.Id);

            //create minMaxes
            minMaxReadings = CreateMinMaxReadings(reading.Device.MinMaxRecords, sensorList, snapShot.Id);

            //create variables
            variables = WeatherMonitorVariableMapper.Map(reading.Device.Variables, snapShot.Id);

            await deviceRepository.AddSensorReadings(sensorReadings);
            await deviceRepository.AddMinMaxReadings(minMaxReadings);
            await deviceRepository.AddVariablesReadings(variables);


            Console.WriteLine("test done");
        }
        private async Task<WeatherMonitor?> GetMonitorEntity(WeatherMonitorDto dto)
        {
            var stored = await deviceRepository.GetBySerialNumber(dto.SerialNumber);
            
            if(stored is not null && stored.Firmware != dto.Firmware)
            {
                stored.Firmware = dto.Firmware;
            }

            return stored;
        }

        private async Task<List<WeatherMonitorSensor>> GetSensorEntities(WeatherMonitor monitor)
            => await deviceRepository.GetSensors(monitor);
            
        private async Task<List<WeatherMonitorSensor>> CreateSensorsFromDto(WeatherMonitorDto dto, Guid monitorId) {

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

            sensors = await deviceRepository.AddSensors(sensors);
            return sensors;

        }

        private async Task<WeatherMonitorSnapshot> CreateSnapshot(WeatherMonitorDto dto, WeatherMonitor entity)
        {
            var snapShot = WeatherMonitorSnapshotMapper.MapSnapShot(dto);

            snapShot.WeatherMonitor = entity;
            snapShot.WeatherMonitorId = entity.Id;

            return await deviceRepository.AddSnapshot(snapShot);
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

        //private WeatherMonitorSnapshot CreateSnapshot()

        /*Flow:
         * 1. Grab Device based on SN - if null, then create new
         *              - anything is different then update
         *              
         * ******** Device done ********
         * 2. Grab sensors - create dictionary for quick mapping
         * 
         * 3. Create Weather Snapshot - link to device
         * 
         * 4. Create Sensor readings, minmaxes & variables & link
         * 5. store all
        */
    }
}
