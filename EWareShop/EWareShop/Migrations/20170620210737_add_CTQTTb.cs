using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EWareShop.Migrations
{
    public partial class add_CTQTTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuaTang",
                columns: table => new
                {
                    QuaTangID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenQuaTang = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuaTang", x => x.QuaTangID);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietQuaTang",
                columns: table => new
                {
                    SanPhamID = table.Column<int>(nullable: false),
                    QuaTangID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietQuaTang", x => new { x.SanPhamID, x.QuaTangID });
                    table.UniqueConstraint("AK_ChiTietQuaTang_QuaTangID_SanPhamID", x => new { x.QuaTangID, x.SanPhamID });
                    table.ForeignKey(
                        name: "FK_ChiTietQuaTang_QuaTang_QuaTangID",
                        column: x => x.QuaTangID,
                        principalTable: "QuaTang",
                        principalColumn: "QuaTangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietQuaTang_SanPham_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPham",
                        principalColumn: "SanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietQuaTang");

            migrationBuilder.DropTable(
                name: "QuaTang");
        }
    }
}
