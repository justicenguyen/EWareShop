using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class TaiKhoan
    {
        [Key]
        public int TaiKhoanID { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Mật khẩu")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string MatKhau { get; set; }
        [DisplayName("Nhập lại mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        [Compare("MatKhau",ErrorMessage ="Không trùng khớp")]
        public string MatKhauNhapLai { get; set; }
        [DisplayName("Họ và tên")]
        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        public string HoTen { get; set; }
        public int Quyen { get; set; }
        public bool DangHoatDong { get; set; }
    }
}
