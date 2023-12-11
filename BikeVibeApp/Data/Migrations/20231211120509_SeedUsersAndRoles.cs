using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeVibeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO dbo.AspNetRoles(Id,Name,NormalizedName) VALUES('915469de-15e7-4f16-ac2a-f8d4e8454009', 'AdminRole', 'ADMINROLE')");
            migrationBuilder.Sql("INSERT INTO dbo.AspNetUsers(Id, UserName, NormalizedUserName,  Email, NormalizedEmail, EmailConfirmed,PhoneNumberConfirmed, LockoutEnabled,TwoFactorEnabled, AccessFailedCount, PasswordHash,SecurityStamp) VALUES('8fe5225d-4a91-4547-9524-be90fb478fe6','administrator@bikevibe.com','ADMINISTRATOR@BIKEVIBE.COM','administrator@bikevibe.com','ADMINISTRATOR@BIKEVIBE.COM',1,0,0,0,0,'AQAAAAIAAYagAAAAEDt4runquxHqWUXCTiBxjW0Vq3R+4/BTVrIqcwC2yLWpmnfUWADr+E4IEP99N+7GIA==','54cbda76-c2ae-4118-82aa-3724cbd2234e')");
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUserRoles]([UserId],[RoleId])VALUES('8fe5225d-4a91-4547-9524-be90fb478fe6','915469de-15e7-4f16-ac2a-f8d4e8454009')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetRoles] WHERE Id = '915469de-15e7-4f16-ac2a-f8d4e8454009'");
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUsers] WHERE Id = '8fe5225d-4a91-4547-9524-be90fb478fe6'");
        }
    }
}
