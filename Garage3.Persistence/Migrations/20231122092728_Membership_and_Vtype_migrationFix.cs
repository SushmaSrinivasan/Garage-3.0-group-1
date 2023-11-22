using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Membership_and_Vtype_migrationFix : Migration
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
                principalColumn: "Id");
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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
