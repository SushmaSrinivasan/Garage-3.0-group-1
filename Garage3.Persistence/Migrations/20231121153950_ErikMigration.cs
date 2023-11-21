using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ErikMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOccupied",
                table: "ParkingSpaces",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ParkingSpot",
                table: "ParkingSpaces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpotNumber",
                table: "ParkingSpaces",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOccupied",
                table: "ParkingSpaces");

            migrationBuilder.DropColumn(
                name: "ParkingSpot",
                table: "ParkingSpaces");

            migrationBuilder.DropColumn(
                name: "SpotNumber",
                table: "ParkingSpaces");
        }
    }
}
