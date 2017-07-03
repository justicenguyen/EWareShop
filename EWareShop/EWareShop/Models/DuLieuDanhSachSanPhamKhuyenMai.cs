using EWareShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class DuLieuDanhSachSanPhamKhuyenMai
    {
        public List<SanPham> DSSanPhamKhuyenMai;
        public List<NhaSanXuat> DSNhaSanXuat;
        public List<LoaiSanPham> DSLoaiSanPham;
        public int SoTrangSanPham;
        public int TrangHienTai;
    }
}
