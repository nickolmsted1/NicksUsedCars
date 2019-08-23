using Microsoft.EntityFrameworkCore.Migrations;

namespace NicksUsedCars.Migrations
{
    public partial class WaitList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleStockNumber",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VehicleStockNumber",
                table: "AspNetUsers",
                column: "VehicleStockNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Vehicles_VehicleStockNumber",
                table: "AspNetUsers",
                column: "VehicleStockNumber",
                principalTable: "Vehicles",
                principalColumn: "StockNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Vehicles_VehicleStockNumber",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_VehicleStockNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VehicleStockNumber",
                table: "AspNetUsers");
        }
    }
}
