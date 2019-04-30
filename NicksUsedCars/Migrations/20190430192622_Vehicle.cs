using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NicksUsedCars.Migrations
{
    public partial class Vehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_EngineType_EngineId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "EngineType");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_EngineId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "EngineId",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "EngineConfiguration",
                table: "Vehicles",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EngineSize",
                table: "Vehicles",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FuelType",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Horsepower",
                table: "Vehicles",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EngineConfiguration",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "EngineSize",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "FuelType",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Horsepower",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "EngineId",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EngineType",
                columns: table => new
                {
                    EngineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EngineConfiguration = table.Column<string>(maxLength: 5, nullable: true),
                    EngineSize = table.Column<string>(maxLength: 5, nullable: true),
                    FuelType = table.Column<string>(maxLength: 15, nullable: true),
                    Horsepower = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineType", x => x.EngineId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_EngineId",
                table: "Vehicles",
                column: "EngineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_EngineType_EngineId",
                table: "Vehicles",
                column: "EngineId",
                principalTable: "EngineType",
                principalColumn: "EngineId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
