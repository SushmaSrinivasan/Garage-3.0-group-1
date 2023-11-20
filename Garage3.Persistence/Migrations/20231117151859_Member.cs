using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Member : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OwnerPersonnummer",
                table: "ParkVehicle",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Personnummer = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Membership = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Personnummer);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkVehicle_OwnerPersonnummer",
                table: "ParkVehicle",
                column: "OwnerPersonnummer");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkVehicle_Member_OwnerPersonnummer",
                table: "ParkVehicle",
                column: "OwnerPersonnummer",
                principalTable: "Member",
                principalColumn: "Personnummer",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkVehicle_Member_OwnerPersonnummer",
                table: "ParkVehicle");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropIndex(
                name: "IX_ParkVehicle_OwnerPersonnummer",
                table: "ParkVehicle");

            migrationBuilder.DropColumn(
                name: "OwnerPersonnummer",
                table: "ParkVehicle");
        }
    }
}
