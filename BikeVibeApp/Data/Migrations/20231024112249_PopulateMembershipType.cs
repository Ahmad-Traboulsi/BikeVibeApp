using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeVibeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class PopulateMembershipType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO dbo.MembershipTypes(Name, Discount,SignUpFee) VALUES ('Pay As You Go',0,0)");
            migrationBuilder.Sql("INSERT INTO dbo.MembershipTypes(Name, Discount,SignUpFee) VALUES ('Monthly',10,30)");
            migrationBuilder.Sql("INSERT INTO dbo.MembershipTypes(Name, Discount,SignUpFee) VALUES ('Quarterly',15,90)");
            migrationBuilder.Sql("INSERT INTO dbo.MembershipTypes(Name, Discount,SignUpFee) VALUES ('Yearly',20,300)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
