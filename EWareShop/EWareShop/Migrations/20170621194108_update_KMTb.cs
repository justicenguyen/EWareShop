using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EWareShop.Migrations
{
    public partial class update_KMTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "HienThi",
                table: "KhuyenMai",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HienThi",
                table: "KhuyenMai",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
