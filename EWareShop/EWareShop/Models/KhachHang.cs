using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class KhachHang
    {
        public int KhachHangID { get; set; }
        [DisplayName("Họ tên khách hàng *")]
        [Required(ErrorMessage = "Trường này là bắt buộc")]
        public string HoTenKhachHang { get; set; }
        [DisplayName("Số điện thoại *")]
        [Required(ErrorMessage = "Trường này là bắt buộc")]
        public string SoDienThoai { get; set; }
        [DisplayName("Địa chỉ ")]
        public string DiaChi { get; set;}
        [DisplayName("Email ")]
        public string Email { get; set; }
    }
}
