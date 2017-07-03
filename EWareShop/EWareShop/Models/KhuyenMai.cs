using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class KhuyenMai
    {
        [Key]
        public int KhuyenMaiID { get; set; }
        [DisplayName("Tiêu đề khuyến mãi *")]
        [Required(ErrorMessage ="Bạn chưa nhập tiêu đề khuyến mãi !")]
        public string TieuDeKhuyenMai { get; set; }
        [Required(ErrorMessage ="Bạn chưa chọn hình ảnh !")]
        [DisplayName("Hình ảnh")]
        public string HinhAnh { get; set; }
        [DisplayName("Nội dung")]
        public string NoiDung { get; set; }
        [DisplayName("Hiển thị trang chủ")]
        public bool HienThi { get; set; }
    }
}
