using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class SanPhamBan
    {
        public int SanPhamBanID { get; set; }
        public int SanPhamID { get; set; }
        public string TenSanPham { get; set; }
        public int SoThangBaoHanh { get; set; }
        public int KhachHangID { get; set; }
        public string MaBaoHanh { get; set; }
        public DateTime NgayBan { get; set; }
        public DateTime NgayHetBaoHanh { get; set; }
    }
}
