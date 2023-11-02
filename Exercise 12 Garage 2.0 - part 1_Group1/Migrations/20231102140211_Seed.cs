using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Exercise_12_Garage_2._0___part_1_Group1.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ParkVehicle",
                columns: new[] { "Id", "Brand", "Color", "Model", "NumberOfWheels", "RegistrationNumber", "VehicleType" },
                values: new object[,]
                {
                    { 1, "Boeing", "White", "A124", 6, "ABC 123", 0 },
                    { 2, "MasterCraft", "Yellow", "011", 0, "DEF 456", 1 },
                    { 3, "Volvo", "Red", "Scania", 4, "GHI 789", 2 },
                    { 4, "Toyota", "Black", "Corolla", 4, "JKL 012", 3 },
                    { 5, "Cresent", "Blue", "92323", 2, "MNO 111", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParkVehicle",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkVehicle",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkVehicle",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkVehicle",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ParkVehicle",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
