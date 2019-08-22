using Microsoft.EntityFrameworkCore.Migrations;

namespace NicksUsedCars.Migrations
{
    public partial class WaitlistManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "VehicleWaitList",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleWaitList", x => new { x.UserId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_VehicleWaitList_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleWaitList_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "StockNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleWaitList_VehicleId",
                table: "VehicleWaitList",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleWaitList");

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
    }
}
