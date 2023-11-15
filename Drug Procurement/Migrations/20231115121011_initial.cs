using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drug_Procurement.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Roles",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            //        DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
                //constraints: table =>
                //{
                //    table.PrimaryKey("PK_Roles", x => x.Id);
                //});

            ////migrationBuilder.CreateTable(
            ////    name: "Users",
            ////    columns: table => new
            ////    {
            ////        Id = table.Column<int>(type: "int", nullable: false)
            ////            .Annotation("SqlServer:Identity", "1, 1"),
            ////        UserName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
            ////        Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
            ////        FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            ////        LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            ////        Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
            ////        RoleId = table.Column<int>(type: "int", nullable: false),
            ////        Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ////        IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            ////        DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
            ////        DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            ////    },
            ////    constraints: table =>
            ////    {
            ////        table.PrimaryKey("PK_Users", x => x.Id);
            ////        table.ForeignKey(
            ////            name: "FK_Users_Roles_RoleId",
            ////            column: x => x.RoleId,
            ////            principalTable: "Roles",
            ////            principalColumn: "Id",
            ////            onDelete: ReferentialAction.Cascade);
            ////    });

            //migrationBuilder.CreateTable(
            //    name: "Inventory",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<int>(type: "int", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ManufacturerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ManufactureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Price = table.Column<double>(type: "float", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            //        DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Inventory", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Inventory_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Order",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
            //        Quantity = table.Column<double>(type: "float", nullable: false),
            //        Price = table.Column<double>(type: "float", nullable: false),
            //        UserId = table.Column<int>(type: "int", nullable: false),
            //        InventoryId = table.Column<int>(type: "int", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            //        DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Order", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Order_Inventory_InventoryId",
            //            column: x => x.InventoryId,
            //            principalTable: "Inventory",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Order_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Inventory_UserId",
        //        table: "Inventory",
        //        column: "UserId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Order_InventoryId",
        //        table: "Order",
        //        column: "InventoryId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Order_UserId",
        //        table: "Order",
        //        column: "UserId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Users_RoleId",
        //        table: "Users",
        //        column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Order");

            //migrationBuilder.DropTable(
            //    name: "Inventory");

            //migrationBuilder.DropTable(
            //    name: "Users");

            //migrationBuilder.DropTable(
            //    name: "Roles");
        }
    }
}
