using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EWareShop.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    DonHangID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DaDuyet = table.Column<bool>(nullable: false),
                    DaGiao = table.Column<bool>(nullable: false),
                    DiaChi = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true),
                    HinhThucThanhToan = table.Column<int>(nullable: false),
                    SoDienThoai = table.Column<string>(nullable: false),
                    TenKhachHang = table.Column<string>(nullable: false),
                    TinhThanhPho = table.Column<int>(nullable: false),
                    TongTien = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.DonHangID);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    KhachHangID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiaChi = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    HoTenKhachHang = table.Column<string>(nullable: false),
                    SoDienThoai = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.KhachHangID);
                });

            migrationBuilder.CreateTable(
                name: "KhuyenMai",
                columns: table => new
                {
                    KhuyenMaiID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HienThi = table.Column<int>(nullable: false),
                    HinhAnh = table.Column<string>(nullable: false),
                    NoiDung = table.Column<string>(nullable: true),
                    TieuDeKhuyenMai = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhuyenMai", x => x.KhuyenMaiID);
                });

            migrationBuilder.CreateTable(
                name: "LoaiSanPham",
                columns: table => new
                {
                    LoaiSanPhamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenLoaiSPCoDau = table.Column<string>(nullable: false),
                    TenLoaiSPKhongDau = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiSanPham", x => x.LoaiSanPhamID);
                });

            migrationBuilder.CreateTable(
                name: "NhaSanXuat",
                columns: table => new
                {
                    NhaSanXuatID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenNSXCoDau = table.Column<string>(nullable: false),
                    TenNSXKhongDau = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaSanXuat", x => x.NhaSanXuatID);
                });

            migrationBuilder.CreateTable(
                name: "PhiGiaoHang",
                columns: table => new
                {
                    PhiGiaoHangID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiaDiem = table.Column<string>(nullable: false),
                    TienPhi = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhiGiaoHang", x => x.PhiGiaoHangID);
                });

            migrationBuilder.CreateTable(
                name: "SanPhamBan",
                columns: table => new
                {
                    SanPhamBanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KhachHangID = table.Column<int>(nullable: false),
                    MaBaoHanh = table.Column<string>(nullable: true),
                    NgayBan = table.Column<DateTime>(nullable: false),
                    NgayHetBaoHanh = table.Column<DateTime>(nullable: false),
                    SanPhamID = table.Column<int>(nullable: false),
                    SoThangBaoHanh = table.Column<int>(nullable: false),
                    TenSanPham = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamBan", x => x.SanPhamBanID);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    SanPhamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaoHanh = table.Column<int>(nullable: false),
                    BoXuLy = table.Column<string>(nullable: true),
                    ChiaSeThongMinh = table.Column<string>(nullable: true),
                    CongHDMI = table.Column<string>(nullable: true),
                    CongInternet = table.Column<string>(nullable: true),
                    CongSuatLoa = table.Column<string>(nullable: true),
                    CongUSB = table.Column<string>(nullable: true),
                    CongWiFi = table.Column<string>(nullable: true),
                    DoPhanGiai = table.Column<string>(nullable: true),
                    GiaBan = table.Column<int>(nullable: false),
                    GiaGiam = table.Column<int>(nullable: false),
                    GiaGoc = table.Column<int>(nullable: false),
                    HeDeHanh = table.Column<string>(nullable: true),
                    HienThi = table.Column<bool>(nullable: false),
                    HinhAnh = table.Column<string>(nullable: false),
                    KhoiLuongThung = table.Column<string>(nullable: true),
                    KichThuocMH = table.Column<string>(nullable: true),
                    KichThuocThung = table.Column<string>(nullable: true),
                    LoaiDanMay = table.Column<string>(nullable: true),
                    LoaiDauDia = table.Column<string>(nullable: true),
                    LoaiSanPhamID = table.Column<int>(nullable: false),
                    ManHinhCong = table.Column<string>(nullable: true),
                    MauSac = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(nullable: true),
                    NgayTao = table.Column<DateTime>(nullable: false),
                    NhaSanXuatID = table.Column<int>(nullable: false),
                    SanPhamBanChay = table.Column<bool>(nullable: false),
                    SmartTV = table.Column<string>(nullable: true),
                    SoLuong = table.Column<int>(nullable: false),
                    TanSoQuet = table.Column<string>(nullable: true),
                    TenSPCoDau = table.Column<string>(nullable: false),
                    TenSPKhongDau = table.Column<string>(nullable: false),
                    TrinhDuyetWeb = table.Column<string>(nullable: true),
                    XuatXu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.SanPhamID);
                    table.ForeignKey(
                        name: "FK_SanPham_LoaiSanPham_LoaiSanPhamID",
                        column: x => x.LoaiSanPhamID,
                        principalTable: "LoaiSanPham",
                        principalColumn: "LoaiSanPhamID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPham_NhaSanXuat_NhaSanXuatID",
                        column: x => x.NhaSanXuatID,
                        principalTable: "NhaSanXuat",
                        principalColumn: "NhaSanXuatID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BinhLuan",
                columns: table => new
                {
                    BinhLuanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    HoTen = table.Column<string>(nullable: false),
                    NoiDung = table.Column<string>(nullable: false),
                    SanPhamID = table.Column<int>(nullable: false),
                    SoDienThoai = table.Column<string>(nullable: true),
                    ThoiGian = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuan", x => x.BinhLuanID);
                    table.ForeignKey(
                        name: "FK_BinhLuan_SanPham_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPham",
                        principalColumn: "SanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    SanPhamID = table.Column<int>(nullable: false),
                    DonHangID = table.Column<int>(nullable: false),
                    Gia = table.Column<int>(nullable: true),
                    SoLuong = table.Column<int>(nullable: true),
                    TongTien = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHang", x => new { x.SanPhamID, x.DonHangID });
                    table.UniqueConstraint("AK_ChiTietDonHang_DonHangID_SanPhamID", x => new { x.DonHangID, x.SanPhamID });
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_DonHang_DonHangID",
                        column: x => x.DonHangID,
                        principalTable: "DonHang",
                        principalColumn: "DonHangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_SanPham_SanPhamID",
                        column: x => x.SanPhamID,
                        principalTable: "SanPham",
                        principalColumn: "SanPhamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuan_SanPhamID",
                table: "BinhLuan",
                column: "SanPhamID");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_LoaiSanPhamID",
                table: "SanPham",
                column: "LoaiSanPhamID");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_NhaSanXuatID",
                table: "SanPham",
                column: "NhaSanXuatID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinhLuan");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "KhuyenMai");

            migrationBuilder.DropTable(
                name: "PhiGiaoHang");

            migrationBuilder.DropTable(
                name: "SanPhamBan");

            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "LoaiSanPham");

            migrationBuilder.DropTable(
                name: "NhaSanXuat");
        }
    }
}
