using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drug_Procurement.Migrations
{
    public partial class inventoryisdeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Inventory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Inventory",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
