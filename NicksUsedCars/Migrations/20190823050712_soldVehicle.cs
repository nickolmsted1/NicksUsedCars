using Microsoft.EntityFrameworkCore.Migrations;

namespace NicksUsedCars.Migrations
{
    public partial class soldVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SoldVehicles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    StockNumber = table.Column<int>(nullable: false),
                    Vin = table.Column<string>(maxLength: 17, nullable: true),
                    Manufacturer = table.Column<string>(maxLength: 50, nullable: false),
                    Model = table.Column<string>(maxLength: 50, nullable: false),
                    ModelType = table.Column<string>(nullable: true),
                    ManufacturedYear = table.Column<int>(nullable: false),
                    BodyStyle = table.Column<string>(maxLength: 50, nullable: true),
                    TransmissionType = table.Column<int>(nullable: false),
                    DriveType = table.Column<int>(nullable: false),
                    ExteriorColor = table.Column<string>(maxLength: 30, nullable: true),
                    InteriorColor = table.Column<string>(maxLength: 30, nullable: true),
                    Mileage = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    PhotoUrl = table.Column<string>(nullable: true),
                    SmallPhotoUrl = table.Column<string>(nullable: true),
                    FuelType = table.Column<int>(nullable: false),
                    EngineConfiguration = table.Column<int>(nullable: false),
                    EngineSize = table.Column<double>(nullable: false),
                    Horsepower = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldVehicles", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_SoldVehicles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoldVehicles");
        }
    }
}
