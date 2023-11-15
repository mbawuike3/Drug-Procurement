using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drug_Procurement.Migrations
{
    public partial class isdeletedaddedforinventorytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Inventory",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Inventory");
        }
    }
}
