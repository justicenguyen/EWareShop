using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EWareShop.Migrations
{
    public partial class update_TKTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenDangNhap",
                table: "TaiKhoan",
                newName: "MatKhau");

            migrationBuilder.RenameColumn(
                name: "MatKhauCap2",
                table: "TaiKhoan",
                newName: "HoTen");

            migrationBuilder.RenameColumn(
                name: "MatKhauCap1",
                table: "TaiKhoan",
                newName: "Email");

            migrationBuilder.AddColumn<int>(
                name: "Quyen",
                table: "TaiKhoan",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quyen",
                table: "TaiKhoan");

            migrationBuilder.RenameColumn(
                name: "MatKhau",
                table: "TaiKhoan",
                newName: "TenDangNhap");

            migrationBuilder.RenameColumn(
                name: "HoTen",
                table: "TaiKhoan",
                newName: "MatKhauCap2");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "TaiKhoan",
                newName: "MatKhauCap1");
        }
    }
}
