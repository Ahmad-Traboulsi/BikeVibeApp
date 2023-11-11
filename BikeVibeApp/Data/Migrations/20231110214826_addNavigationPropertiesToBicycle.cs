using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeVibeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addNavigationPropertiesToBicycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "Bicycles",
                newName: "Dateadded");

            migrationBuilder.AlterColumn<double>(
                name: "DailyRentalRate",
                table: "Bicycles",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<byte>(
                name: "BicycleTypeId",
                table: "Bicycles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<short>(
                name: "BrandId",
                table: "Bicycles",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_Bicycles_BicycleTypeId",
                table: "Bicycles",
                column: "BicycleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bicycles_BrandId",
                table: "Bicycles",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bicycles_BicycleTypes_BicycleTypeId",
                table: "Bicycles",
                column: "BicycleTypeId",
                principalTable: "BicycleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bicycles_Brands_BrandId",
                table: "Bicycles",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bicycles_BicycleTypes_BicycleTypeId",
                table: "Bicycles");

            migrationBuilder.DropForeignKey(
                name: "FK_Bicycles_Brands_BrandId",
                table: "Bicycles");

            migrationBuilder.DropIndex(
                name: "IX_Bicycles_BicycleTypeId",
                table: "Bicycles");

            migrationBuilder.DropIndex(
                name: "IX_Bicycles_BrandId",
                table: "Bicycles");

            migrationBuilder.DropColumn(
                name: "BicycleTypeId",
                table: "Bicycles");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Bicycles");

            migrationBuilder.RenameColumn(
                name: "Dateadded",
                table: "Bicycles",
                newName: "DateAdded");

            migrationBuilder.AlterColumn<decimal>(
                name: "DailyRentalRate",
                table: "Bicycles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
