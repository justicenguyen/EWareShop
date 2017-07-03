using EWareShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class DuLieuTrangChu
    {
        public SanPham SanPham;
        public LoaiSanPham LoaiSanPham;
        public List<LoaiSanPham> DSLoaiSanPham;
        public List<SanPham> DSSanPhamHienThi;
        public List<SanPham> DSSanPhamMoiNhat;
        public List<SanPham> DSSanPhamBanChay;
        public List<KhuyenMai> DSKhuyenMai;
    }
}
