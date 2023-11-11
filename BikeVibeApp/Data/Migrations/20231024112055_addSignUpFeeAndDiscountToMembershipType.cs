using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeVibeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSignUpFeeAndDiscountToMembershipType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Discount",
                table: "MembershipTypes",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<decimal>(
                name: "SignUpFee",
                table: "MembershipTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "MembershipTypes");

            migrationBuilder.DropColumn(
                name: "SignUpFee",
                table: "MembershipTypes");
        }
    }
}
