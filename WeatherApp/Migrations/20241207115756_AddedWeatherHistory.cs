using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedWeatherHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RetrievedAt",
                table: "WeatherResults");

            migrationBuilder.AddColumn<double>(
                name: "Humidity",
                table: "WeatherResults",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "WeatherHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WeatherDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherHistory_WeatherResults_WeatherDataId",
                        column: x => x.WeatherDataId,
                        principalTable: "WeatherResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherHistory_WeatherDataId",
                table: "WeatherHistory",
                column: "WeatherDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherHistory");

            migrationBuilder.DropColumn(
                name: "Humidity",
                table: "WeatherResults");

            migrationBuilder.AddColumn<DateTime>(
                name: "RetrievedAt",
                table: "WeatherResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
