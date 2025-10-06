# Weather Monitor Data Reader

Console application that pulls data from Weather Monitor via HTTP in XML format, converts it to JSON, and stores it in a database.  
The application processes data on startup and then repeats at a configurable interval.

## Data Flow
1. **Pull Data**: Fetch XML from the monitor.
2. **Deserialize**: Convert XML to JSON DTO.
3. **Initialize Monitor**:
   - Look for monitor in the database by Serial Number (SN).
   - If none exists, create it from JSON DTO and store in Database.
4. **Initialize Sensors**:
   - Pull sensors associated with the monitor from DB.
   - If the monitor is new, create sensors from JSON and store in Database.
5. **Initialize and store Readings**:
   - Store sensor readings, min/max values, and variables.

### Monitor Unreachable
- If previously reachable: create a new reading marking the monitor down.
- If never reachable (unknown SN): no record is created.

## Running the application
### Requirements
- Docker & Docker Compose

### Demo - Runs Database, Reader and very simplistic API providing weather station output

1. Clone the repository:
   ```PowerShell
   git clone https://github.com/BrozDa/WeatherMonitorReader.git
   ```
   
2. Run Docker Compose to start reader, DB, and demo API:
   ```PowerShell
   docker compose up -d --build
   ```
   
3. To see the reader in real time: 
   ```PowerShell
   docker attach weathermonitorreader-monitor-reader-1
   ```

4. To simulate an unavailable monitor:
   ```PowerShell
   docker container stop weathermonitorreader-monitor-api-1
   ```

Note: Container names may vary depending on your Docker Compose setup.


### Running the reader separately

You can run the application using the Docker image available on Docker Hub: [broziss/weather-monitor-reader](https://hub.docker.com/repository/docker/broziss/weather-monitor-reader/general).
To do this, you need to set three environment variables:
  1. ConnectionStrings__DefaultConnection - database connection string
  2. ReaderSettings__Url - monitor URL
  3. ReaderSettings__Interval - data pull frequency (seconds)

Example:
```PowerShell
docker container run `
-e "ConnectionStrings__DefaultConnection=Server=db;Database=WeatherMonitor;User Id=sa;Password=TestP4ss!;TrustServerCertificate=True;" `
-e "ReaderSettings__Url=http://123.45.67.89/get-data" `
-e "ReaderSettings__Interval=10" `
--network monitor-net `
--name reader `
broziss/weather-monitor-reader
```
## Database Schema
<img width="898" height="795" alt="obrazek" src="https://github.com/user-attachments/assets/fb85eb28-00c9-4fa6-9606-1291a7a46431" />

