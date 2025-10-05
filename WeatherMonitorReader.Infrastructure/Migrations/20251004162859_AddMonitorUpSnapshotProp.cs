using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherMonitorReader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMonitorUpSnapshotProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Runtime",
                table: "WeatherMonitorSnapshots",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Freemem",
                table: "WeatherMonitorSnapshots",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsMonitorResponding",
                table: "WeatherMonitorSnapshots",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMonitorResponding",
                table: "WeatherMonitorSnapshots");

            migrationBuilder.AlterColumn<int>(
                name: "Runtime",
                table: "WeatherMonitorSnapshots",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Freemem",
                table: "WeatherMonitorSnapshots",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}