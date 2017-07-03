﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EWareShop.Models;

namespace EWareShop.Migrations
{
    [DbContext(typeof(EWareShopContext))]
    [Migration("20170622011111_add_LHTb")]
    partial class add_LHTb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EWareShop.Models.BinhLuan", b =>
                {
                    b.Property<int>("BinhLuanID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("HoTen")
                        .IsRequired();

                    b.Property<bool>("KiemDuyet");

                    b.Property<string>("NoiDung")
                        .IsRequired();

                    b.Property<int>("SanPhamID");

                    b.Property<string>("SoDienThoai");

                    b.Property<int>("SoSao");

                    b.Property<DateTime>("ThoiGian");

                    b.HasKey("BinhLuanID");

                    b.HasIndex("SanPhamID");

                    b.ToTable("BinhLuan");
                });

            modelBuilder.Entity("EWareShop.Models.ChiTietDonHang", b =>
                {
                    b.Property<int>("SanPhamID");

                    b.Property<int>("DonHangID");

                    b.Property<int?>("Gia");

                    b.Property<int?>("SoLuong");

                    b.Property<int?>("TongTien");

                    b.HasKey("SanPhamID", "DonHangID");

                    b.HasAlternateKey("DonHangID", "SanPhamID");

                    b.ToTable("ChiTietDonHang");
                });

            modelBuilder.Entity("EWareShop.Models.ChiTietQuaTang", b =>
                {
                    b.Property<int>("SanPhamID");

                    b.Property<int>("QuaTangID");

                    b.HasKey("SanPhamID", "QuaTangID");

                    b.HasAlternateKey("QuaTangID", "SanPhamID");

                    b.ToTable("ChiTietQuaTang");
                });

            modelBuilder.Entity("EWareShop.Models.DonHang", b =>
                {
                    b.Property<int>("DonHangID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("DaDuyet");

                    b.Property<bool>("DaGiao");

                    b.Property<string>("DiaChi")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("GhiChu");

                    b.Property<DateTime>("NgayDat");

                    b.Property<string>("SoDienThoai")
                        .IsRequired();

                    b.Property<string>("TenKhachHang")
                        .IsRequired();

                    b.Property<int>("TinhThanhPho");

                    b.Property<int>("TongTien");

                    b.HasKey("DonHangID");

                    b.ToTable("DonHang");
                });

            modelBuilder.Entity("EWareShop.Models.KhachHang", b =>
                {
                    b.Property<int>("KhachHangID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DiaChi");

                    b.Property<string>("Email");

                    b.Property<string>("HoTenKhachHang")
                        .IsRequired();

                    b.Property<string>("SoDienThoai")
                        .IsRequired();

                    b.HasKey("KhachHangID");

                    b.ToTable("KhachHang");
                });

            modelBuilder.Entity("EWareShop.Models.KhuyenMai", b =>
                {
                    b.Property<int>("KhuyenMaiID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("HienThi");

                    b.Property<string>("HinhAnh")
                        .IsRequired();

                    b.Property<string>("NoiDung");

                    b.Property<string>("TieuDeKhuyenMai")
                        .IsRequired();

                    b.HasKey("KhuyenMaiID");

                    b.ToTable("KhuyenMai");
                });

            modelBuilder.Entity("EWareShop.Models.LienHe", b =>
                {
                    b.Property<int>("LienHeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("HoTen")
                        .IsRequired();

                    b.Property<DateTime>("NgayGui");

                    b.Property<string>("SoDienThoai");

                    b.Property<string>("VanDe")
                        .IsRequired();

                    b.HasKey("LienHeID");

                    b.ToTable("LienHe");
                });

            modelBuilder.Entity("EWareShop.Models.LoaiSanPham", b =>
                {
                    b.Property<int>("LoaiSanPhamID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TenLoaiSPCoDau")
                        .IsRequired();

                    b.Property<string>("TenLoaiSPKhongDau")
                        .IsRequired();

                    b.HasKey("LoaiSanPhamID");

                    b.ToTable("LoaiSanPham");
                });

            modelBuilder.Entity("EWareShop.Models.NhaSanXuat", b =>
                {
                    b.Property<int>("NhaSanXuatID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TenNSXCoDau")
                        .IsRequired();

                    b.Property<string>("TenNSXKhongDau")
                        .IsRequired();

                    b.HasKey("NhaSanXuatID");

                    b.ToTable("NhaSanXuat");
                });

            modelBuilder.Entity("EWareShop.Models.PhiGiaoHang", b =>
                {
                    b.Property<int>("PhiGiaoHangID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DiaDiem")
                        .IsRequired();

                    b.Property<int>("TienPhi");

                    b.HasKey("PhiGiaoHangID");

                    b.ToTable("PhiGiaoHang");
                });

            modelBuilder.Entity("EWareShop.Models.QuaTang", b =>
                {
                    b.Property<int>("QuaTangID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TenQuaTang")
                        .IsRequired();

                    b.HasKey("QuaTangID");

                    b.ToTable("QuaTang");
                });

            modelBuilder.Entity("EWareShop.Models.SanPham", b =>
                {
                    b.Property<int>("SanPhamID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BaoHanh");

                    b.Property<string>("BoXuLy");

                    b.Property<string>("ChiaSeThongMinh");

                    b.Property<string>("CongHDMI");

                    b.Property<string>("CongInternet");

                    b.Property<string>("CongSuatLoa");

                    b.Property<string>("CongUSB");

                    b.Property<string>("CongWiFi");

                    b.Property<string>("DoPhanGiai");

                    b.Property<int>("GiaBan");

                    b.Property<int>("GiaGiam");

                    b.Property<int>("GiaGoc");

                    b.Property<string>("HeDeHanh");

                    b.Property<bool>("HienThi");

                    b.Property<string>("HinhAnh")
                        .IsRequired();

                    b.Property<string>("KhoiLuongThung");

                    b.Property<string>("KichThuocMH");

                    b.Property<string>("KichThuocThung");

                    b.Property<string>("LoaiDanMay");

                    b.Property<string>("LoaiDauDia");

                    b.Property<int>("LoaiSanPhamID");

                    b.Property<string>("ManHinhCong");

                    b.Property<string>("MauSac");

                    b.Property<string>("MoTa");

                    b.Property<DateTime>("NgayTao");

                    b.Property<int>("NhaSanXuatID");

                    b.Property<bool>("SanPhamBanChay");

                    b.Property<string>("SmartTV");

                    b.Property<int>("SoLuong");

                    b.Property<string>("TanSoQuet");

                    b.Property<string>("TenSPCoDau")
                        .IsRequired();

                    b.Property<string>("TenSPKhongDau")
                        .IsRequired();

                    b.Property<string>("TrinhDuyetWeb");

                    b.Property<string>("XuatXu");

                    b.HasKey("SanPhamID");

                    b.HasIndex("LoaiSanPhamID");

                    b.HasIndex("NhaSanXuatID");

                    b.ToTable("SanPham");
                });

            modelBuilder.Entity("EWareShop.Models.SanPhamBan", b =>
                {
                    b.Property<int>("SanPhamBanID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("KhachHangID");

                    b.Property<string>("MaBaoHanh");

                    b.Property<DateTime>("NgayBan");

                    b.Property<DateTime>("NgayHetBaoHanh");

                    b.Property<int>("SanPhamID");

                    b.Property<int>("SoThangBaoHanh");

                    b.Property<string>("TenSanPham");

                    b.HasKey("SanPhamBanID");

                    b.ToTable("SanPhamBan");
                });

            modelBuilder.Entity("EWareShop.Models.BinhLuan", b =>
                {
                    b.HasOne("EWareShop.Models.SanPham", "SanPham")
                        .WithMany("BinhLuans")
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EWareShop.Models.ChiTietDonHang", b =>
                {
                    b.HasOne("EWareShop.Models.DonHang", "DonHang")
                        .WithMany("ChiTietDonHangs")
                        .HasForeignKey("DonHangID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EWareShop.Models.SanPham", "SanPham")
                        .WithMany("ChiTietDonHangs")
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EWareShop.Models.ChiTietQuaTang", b =>
                {
                    b.HasOne("EWareShop.Models.QuaTang", "QuaTang")
                        .WithMany("ChiTietQuaTangs")
                        .HasForeignKey("QuaTangID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EWareShop.Models.SanPham", "SanPham")
                        .WithMany("ChiTietQuaTang")
                        .HasForeignKey("SanPhamID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EWareShop.Models.SanPham", b =>
                {
                    b.HasOne("EWareShop.Models.LoaiSanPham", "LoaiSanPham")
                        .WithMany("SanPhams")
                        .HasForeignKey("LoaiSanPhamID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EWareShop.Models.NhaSanXuat", "NhaSanXuat")
                        .WithMany("SanPhams")
                        .HasForeignKey("NhaSanXuatID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
