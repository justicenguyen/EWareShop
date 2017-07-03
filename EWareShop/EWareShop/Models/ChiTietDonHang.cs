using EWareShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class ChiTietDonHang
    {
        [Key, Column(Order = 0)]
        public int SanPhamID { get; set; }
        [Key, Column(Order = 1)]
        public int DonHangID { get; set; }
        public int? Gia { get; set; }
        public int? SoLuong { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public int? TongTien { get; set; }
        public SanPham SanPham { get; set; }
        public DonHang DonHang { get; set; }
    }
}
