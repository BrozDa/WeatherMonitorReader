using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherMonitorReader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSensorReadingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherMonitorSensorReading_WeatherMonitorSensors_SensorId",
                table: "WeatherMonitorSensorReading");

            migrationBuilder.DropForeignKey(
                name: "FK_WeatherMonitorSensorReading_WeatherMonitorSnapshots_SnapshotId",
                table: "WeatherMonitorSensorReading");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeatherMonitorSensorReading",
                table: "WeatherMonitorSensorReading");

            migrationBuilder.RenameTable(
                name: "WeatherMonitorSensorReading",
                newName: "WeatherMonitorSensorReadings");

            migrationBuilder.RenameIndex(
                name: "IX_WeatherMonitorSensorReading_SnapshotId",
                table: "WeatherMonitorSensorReadings",
                newName: "IX_WeatherMonitorSensorReadings_SnapshotId");

            migrationBuilder.RenameIndex(
                name: "IX_WeatherMonitorSensorReading_SensorId",
                table: "WeatherMonitorSensorReadings",
                newName: "IX_WeatherMonitorSensorReadings_SensorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeatherMonitorSensorReadings",
                table: "WeatherMonitorSensorReadings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherMonitorSensorReadings_WeatherMonitorSensors_SensorId",
                table: "WeatherMonitorSensorReadings",
                column: "SensorId",
                principalTable: "WeatherMonitorSensors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherMonitorSensorReadings_WeatherMonitorSnapshots_SnapshotId",
                table: "WeatherMonitorSensorReadings",
                column: "SnapshotId",
                principalTable: "WeatherMonitorSnapshots",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherMonitorSensorReadings_WeatherMonitorSensors_SensorId",
                table: "WeatherMonitorSensorReadings");

            migrationBuilder.DropForeignKey(
                name: "FK_WeatherMonitorSensorReadings_WeatherMonitorSnapshots_SnapshotId",
                table: "WeatherMonitorSensorReadings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeatherMonitorSensorReadings",
                table: "WeatherMonitorSensorReadings");

            migrationBuilder.RenameTable(
                name: "WeatherMonitorSensorReadings",
                newName: "WeatherMonitorSensorReading");

            migrationBuilder.RenameIndex(
                name: "IX_WeatherMonitorSensorReadings_SnapshotId",
                table: "WeatherMonitorSensorReading",
                newName: "IX_WeatherMonitorSensorReading_SnapshotId");

            migrationBuilder.RenameIndex(
                name: "IX_WeatherMonitorSensorReadings_SensorId",
                table: "WeatherMonitorSensorReading",
                newName: "IX_WeatherMonitorSensorReading_SensorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeatherMonitorSensorReading",
                table: "WeatherMonitorSensorReading",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherMonitorSensorReading_WeatherMonitorSensors_SensorId",
                table: "WeatherMonitorSensorReading",
                column: "SensorId",
                principalTable: "WeatherMonitorSensors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherMonitorSensorReading_WeatherMonitorSnapshots_SnapshotId",
                table: "WeatherMonitorSensorReading",
                column: "SnapshotId",
                principalTable: "WeatherMonitorSnapshots",
                principalColumn: "Id");
        }
    }
}