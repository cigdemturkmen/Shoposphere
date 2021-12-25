using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shoposphere.Data.Migrations
{
    public partial class PicPropertyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "Users");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Suppliers",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "UserRole",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Suppliers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);
        }
    }
}
