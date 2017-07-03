using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class LienHe
    {
        public int LienHeID { get; set; }
        [Required(ErrorMessage ="Trường này là bắt buộc !")]
        [DisplayName("Họ và tên*")]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "Trường này là bắt buộc !")]
        [DisplayName("Email*")]
        public string Email { get; set; }
        [DisplayName("Số điện thoại")]
        public string SoDienThoai { get; set; }
        [Required(ErrorMessage = "Trường này là bắt buộc !")]
        [DisplayName("Vấn đề cần liên hệ *")]
        public string VanDe { get; set; }
        public DateTime NgayGui { get; set; }
        [DisplayName("Đã trả lời")]
        public bool DaTraLoi { get; set; }
    }
}
