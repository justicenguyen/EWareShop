using EWareShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class DuLieuChiTietSanPham
    {
        public SanPham SanPham;
        public List<SanPham> DSSanPhamLienQuan;
        public string HangSanXuat;
        public List<BinhLuan> DSBinhLuan;
        public List<QuaTang> DSQuaTang;
        public int MucDanhGia;
    }
}
