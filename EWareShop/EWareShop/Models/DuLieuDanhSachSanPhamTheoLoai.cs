using EWareShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class DuLieuDanhSachSanPhamTheoLoai
    {
        public List<SanPham> DSSanPhamTheoLoai;
        public List<NhaSanXuat> DSNhaSanXuat;
        public LoaiSanPham LoaiSanPham;
        public int SoTrangSanPham;
        public int TrangHienTai;
    }
}
