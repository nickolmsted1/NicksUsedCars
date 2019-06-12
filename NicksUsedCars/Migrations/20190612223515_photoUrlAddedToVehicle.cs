using Microsoft.EntityFrameworkCore.Migrations;

namespace NicksUsedCars.Migrations
{
    public partial class photoUrlAddedToVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Vehicles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Vehicles");
        }
    }
}
