using EWareShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class DuLieuChiTietDonHang
    {
        public DonHang DonHang { get; set; }
        public List<MatHang> ChiTietDonHang { get; set; }
    }
}
