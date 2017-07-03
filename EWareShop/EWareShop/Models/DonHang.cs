using EWareShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class DonHang
    {
        public int DonHangID { get; set; }
        [DisplayName("Họ tên khách hàng *")]
        [Required(ErrorMessage = "Trường này là bắt buộc !")]
        public string TenKhachHang{get;set;}
        [DisplayName("Số điện thoại *")]
        [Required(ErrorMessage = "Trường này là bắt buộc !")]
        public string SoDienThoai { get; set;}
        public string Email { get; set; }
        [DisplayName("Tỉnh / Thành phố *")]
        [Required(ErrorMessage = "Trường này là bắt buộc !")]
        public int TinhThanhPho { get; set; }
        [DisplayName("Địa chỉ nhận hàng*")]
        [Required(ErrorMessage = "Trường này là bắt buộc !")]
        public string DiaChi { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public int TongTien { get; set; }
        public DateTime NgayDat { get; set; }
        [DisplayName("Ghi chú")]
        public string GhiChu { get; set;}
        public bool DaDuyet { get; set;}
        public bool DaGiao { get; set;}

        public List<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
