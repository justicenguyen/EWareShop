using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class BinhLuan
    {
        public int BinhLuanID { get; set; }
        public int SanPhamID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Họ và Tên")]
        public string HoTen {get;set;}
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập nội dung bình luận")]
        public string NoiDung { get; set; }
        public int SoSao { get; set; }
        public bool KiemDuyet { get; set; }
        public DateTime ThoiGian { get; set; }
        public SanPham SanPham { get; set; }
    }
}
