using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ParkingSpaceParkVehicleNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpaces_ParkVehicle_ParkVehicleId",
                table: "ParkingSpaces");

            migrationBuilder.AlterColumn<int>(
                name: "ParkVehicleId",
                table: "ParkingSpaces",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpaces_ParkVehicle_ParkVehicleId",
                table: "ParkingSpaces",
                column: "ParkVehicleId",
                principalTable: "ParkVehicle",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpaces_ParkVehicle_ParkVehicleId",
                table: "ParkingSpaces");

            migrationBuilder.AlterColumn<int>(
                name: "ParkVehicleId",
                table: "ParkingSpaces",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpaces_ParkVehicle_ParkVehicleId",
                table: "ParkingSpaces",
                column: "ParkVehicleId",
                principalTable: "ParkVehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
