using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusBookingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBusEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    BusNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Route = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.BusNumber);
                });

            migrationBuilder.InsertData(
                table: "Buses",
                columns: new[] { "BusNumber", "Capacity", "IsActive", "Route" },
                values: new object[,]
                {
                    { "BUS001", 50, true, "City Center - Airport" },
                    { "BUS002", 45, true, "North Station - South Station" },
                    { "BUS003", 40, true, "East Terminal - West Terminal" },
                    { "BUS004", 35, true, "Shopping Mall - University" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buses");
        }
    }
}
