using EWareShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class MatHang
    {
        public SanPham SanPham { get; set; }
        public int? SoLuong { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public int? TongTien { get; set; }
    }
}
