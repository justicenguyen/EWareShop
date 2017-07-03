using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EWareShop.Migrations
{
    public partial class update_DHTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HinhThucThanhToan",
                table: "DonHang");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDat",
                table: "DonHang",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayDat",
                table: "DonHang");

            migrationBuilder.AddColumn<int>(
                name: "HinhThucThanhToan",
                table: "DonHang",
                nullable: false,
                defaultValue: 0);
        }
    }
}
