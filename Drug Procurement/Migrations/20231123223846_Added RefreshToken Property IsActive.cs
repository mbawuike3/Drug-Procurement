using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drug_Procurement.Migrations
{
    public partial class AddedRefreshTokenPropertyIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "LoginRefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "LoginRefreshTokens");
        }
    }
}
