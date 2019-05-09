using Microsoft.EntityFrameworkCore.Migrations;

namespace NicksUsedCars.Migrations
{
    public partial class addedPriceToVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TransmissionType",
                table: "Vehicles",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FuelType",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "EngineSize",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EngineConfiguration",
                table: "Vehicles",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DriveType",
                table: "Vehicles",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Vehicles");

            migrationBuilder.AlterColumn<string>(
                name: "TransmissionType",
                table: "Vehicles",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "FuelType",
                table: "Vehicles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "EngineSize",
                table: "Vehicles",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "EngineConfiguration",
                table: "Vehicles",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "DriveType",
                table: "Vehicles",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 3);
        }
    }
}
