using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EWareShop.Models;

namespace EWareShop.Models
{
    public class EWareShopContext : DbContext
    {
        public EWareShopContext (DbContextOptions<EWareShopContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDonHang>()
                .HasKey(k => new { k.SanPhamID, k.DonHangID });
            modelBuilder.Entity<ChiTietQuaTang>()
                .HasKey(k => new { k.SanPhamID, k.QuaTangID });
        }
        public DbSet<EWareShop.Models.BinhLuan> BinhLuan { get; set; }
        public DbSet<EWareShop.Models.ChiTietDonHang> ChiTietDonHang { get; set; }
        public DbSet<EWareShop.Models.DonHang> DonHang { get; set; }
        public DbSet<EWareShop.Models.KhachHang> KhachHang { get; set; }
        public DbSet<EWareShop.Models.KhuyenMai> KhuyenMai { get; set; }
        public DbSet<EWareShop.Models.LoaiSanPham> LoaiSanPham { get; set; }
        public DbSet<EWareShop.Models.NhaSanXuat> NhaSanXuat { get; set; }
        public DbSet<EWareShop.Models.PhiGiaoHang> PhiGiaoHang { get; set; }
        public DbSet<EWareShop.Models.SanPham> SanPham { get; set; }
        public DbSet<EWareShop.Models.SanPhamBan> SanPhamBan { get; set; }
        public DbSet<EWareShop.Models.ChiTietQuaTang> ChiTietQuaTang { get; set; }
        public DbSet<EWareShop.Models.QuaTang> QuaTang { get; set; }
        public DbSet<EWareShop.Models.LienHe> LienHe { get; set; }
        public DbSet<EWareShop.Models.TaiKhoan> TaiKhoan { get; set; }
       
    }
}
