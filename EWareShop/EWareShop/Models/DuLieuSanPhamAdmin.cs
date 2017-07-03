using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class DuLieuSanPhamAdmin
    {
        public SanPham SanPham;
        public List<QuaTang> DanhSachQuaTangCuaSanPham;
        public List<QuaTang> DanhSachTatCaQuaTang;
    }
}
