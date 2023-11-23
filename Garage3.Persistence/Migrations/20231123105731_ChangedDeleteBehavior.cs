using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpaces_ParkVehicle_VehicleId",
                table: "ParkingSpaces");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpaces_ParkVehicle_VehicleId",
                table: "ParkingSpaces",
                column: "VehicleId",
                principalTable: "ParkVehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpaces_ParkVehicle_VehicleId",
                table: "ParkingSpaces");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpaces_ParkVehicle_VehicleId",
                table: "ParkingSpaces",
                column: "VehicleId",
                principalTable: "ParkVehicle",
                principalColumn: "Id");
        }
    }
}
