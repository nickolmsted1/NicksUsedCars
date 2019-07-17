using Microsoft.EntityFrameworkCore.Migrations;

namespace NicksUsedCars.Migrations
{
    public partial class addSmallPhotoUrlToVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SmallPhotoUrl",
                table: "Vehicles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SmallPhotoUrl",
                table: "Vehicles");
        }
    }
}
