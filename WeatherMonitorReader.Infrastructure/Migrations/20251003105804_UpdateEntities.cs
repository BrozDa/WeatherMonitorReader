using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherMonitorReader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherMonitors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DegreeUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PressureUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Firmware = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PressureType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    R = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bip = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherMonitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherMonitorSensors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeatherMonitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SensorId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherMonitorSensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherMonitorSensors_WeatherMonitors_WeatherMonitorId",
                        column: x => x.WeatherMonitorId,
                        principalTable: "WeatherMonitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeatherMonitorSnapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeatherMonitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Runtime = table.Column<int>(type: "int", nullable: false),
                    Freemem = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherMonitorSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherMonitorSnapshots_WeatherMonitors_WeatherMonitorId",
                        column: x => x.WeatherMonitorId,
                        principalTable: "WeatherMonitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeatherMonitorSensorReading",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SensorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SnapshotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherMonitorSensorReading", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherMonitorSensorReading_WeatherMonitorSensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "WeatherMonitorSensors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WeatherMonitorSensorReading_WeatherMonitorSnapshots_SnapshotId",
                        column: x => x.SnapshotId,
                        principalTable: "WeatherMonitorSnapshots",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeatherMonitorSnapshotMinMaxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SensorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SnapshotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Min = table.Column<double>(type: "float", nullable: false),
                    Max = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherMonitorSnapshotMinMaxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherMonitorSnapshotMinMaxes_WeatherMonitorSensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "WeatherMonitorSensors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WeatherMonitorSnapshotMinMaxes_WeatherMonitorSnapshots_SnapshotId",
                        column: x => x.SnapshotId,
                        principalTable: "WeatherMonitorSnapshots",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeatherMonitorVariables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeatherMonitorSnapshotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sunrise = table.Column<TimeOnly>(type: "time", nullable: false),
                    Sunset = table.Column<TimeOnly>(type: "time", nullable: false),
                    Civstart = table.Column<TimeOnly>(type: "time", nullable: false),
                    Civend = table.Column<TimeOnly>(type: "time", nullable: false),
                    Nautstart = table.Column<TimeOnly>(type: "time", nullable: false),
                    Nautend = table.Column<TimeOnly>(type: "time", nullable: false),
                    Astrostart = table.Column<TimeOnly>(type: "time", nullable: false),
                    Astroend = table.Column<TimeOnly>(type: "time", nullable: false),
                    Daylen = table.Column<TimeOnly>(type: "time", nullable: false),
                    Civlen = table.Column<TimeOnly>(type: "time", nullable: false),
                    Nautlen = table.Column<TimeOnly>(type: "time", nullable: false),
                    Astrolen = table.Column<TimeOnly>(type: "time", nullable: false),
                    Moonphase = table.Column<int>(type: "int", nullable: false),
                    IsDay = table.Column<bool>(type: "bit", nullable: false),
                    Bio = table.Column<int>(type: "int", nullable: false),
                    PressureOld = table.Column<double>(type: "float", nullable: false),
                    TemperatureAvg = table.Column<double>(type: "float", nullable: false),
                    Agl = table.Column<int>(type: "int", nullable: false),
                    Fog = table.Column<int>(type: "int", nullable: false),
                    Lsp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherMonitorVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherMonitorVariables_WeatherMonitorSnapshots_WeatherMonitorSnapshotId",
                        column: x => x.WeatherMonitorSnapshotId,
                        principalTable: "WeatherMonitorSnapshots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherMonitorSensorReading_SensorId",
                table: "WeatherMonitorSensorReading",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherMonitorSensorReading_SnapshotId",
                table: "WeatherMonitorSensorReading",
                column: "SnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherMonitorSensors_WeatherMonitorId",
                table: "WeatherMonitorSensors",
                column: "WeatherMonitorId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherMonitorSnapshotMinMaxes_SensorId",
                table: "WeatherMonitorSnapshotMinMaxes",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherMonitorSnapshotMinMaxes_SnapshotId",
                table: "WeatherMonitorSnapshotMinMaxes",
                column: "SnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherMonitorSnapshots_WeatherMonitorId",
                table: "WeatherMonitorSnapshots",
                column: "WeatherMonitorId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherMonitorVariables_WeatherMonitorSnapshotId",
                table: "WeatherMonitorVariables",
                column: "WeatherMonitorSnapshotId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherMonitorSensorReading");

            migrationBuilder.DropTable(
                name: "WeatherMonitorSnapshotMinMaxes");

            migrationBuilder.DropTable(
                name: "WeatherMonitorVariables");

            migrationBuilder.DropTable(
                name: "WeatherMonitorSensors");

            migrationBuilder.DropTable(
                name: "WeatherMonitorSnapshots");

            migrationBuilder.DropTable(
                name: "WeatherMonitors");
        }
    }
}
