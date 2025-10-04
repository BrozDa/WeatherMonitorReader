using System.Xml.Linq;
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
        IDeviceRepository deviceRepository)
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

            if (monitor is null) //new monitor -> create monitor and sensors
            {
                monitor = await deviceRepository
                    .Add(WeatherMonitorMapper.MapMonitor(reading.Device));

                sensorList = CreateSensorsFromDto(reading.Device);
            }
            else //existing - retrieve list
            {
                sensorList = await GetSensorEntities(monitor);
            }
           
            //create snapshot


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
            
        private List<WeatherMonitorSensor> CreateSensorsFromDto(WeatherMonitorDto dto) {

            List<WeatherMonitorSensor> sensors = new();

            foreach (var sensorDto in dto.Input.Sensors) {
                sensors.Add(
                    WeatherMonitorSensorMapper
                    .MapSensor(sensorDto, SensorDirection.Input));
            }
            foreach (var sensorDto in dto.Output.Sensors)
            {
                sensors.Add(
                    WeatherMonitorSensorMapper
                    .MapSensor(sensorDto, SensorDirection.Output));
            }

            return sensors;

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
