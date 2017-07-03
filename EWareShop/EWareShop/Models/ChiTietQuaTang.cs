using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class ChiTietQuaTang
    {
        [Key, Column(Order = 0)]
        public int SanPhamID { get; set; }
        [Key, Column(Order = 1)]
        public int QuaTangID { get; set; }

        public QuaTang QuaTang { get; set; }
        public SanPham SanPham { get; set; }
    }
}
