using EWareShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class SanPham
    {
        // Thong tin chung
        [Key]
        public int SanPhamID { get; set; }
        [DisplayName("Tên sản phẩm có dấu *")]
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm có dấu !")]
        public string TenSPCoDau { get; set; }
        [DisplayName("Tên sản phẩm không dấu *")]
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm có không dấu !")]
        public string TenSPKhongDau { get; set; }
        [DisplayName("Giá gốc *")]
        [Range(0, int.MaxValue, ErrorMessage = "Giá gốc sản phẩm không hợp lệ")]
        [Required(ErrorMessage = "Bạn chưa nhập giá gốc sản phẩm !")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public int GiaGoc { get; set; }
        [DisplayName("Giá khuyến mãi *")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public int GiaGiam { get; set; }
        [DisplayName("Giá Bán")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public int GiaBan { get; set; }
        [DisplayName("Hình đại diện* ")]
        [Required(ErrorMessage = "Bạn chưa chọn ảnh đại diện cho sản phẩm !")]
        public string HinhAnh { get; set;}
        [DisplayName("Loại sản phẩm *")]
        public int LoaiSanPhamID { get; set; }
        [DisplayName("Màu sắc")]
        public string MauSac { get; set; }
        [DisplayName("Nhà sản xuất")]
        public int NhaSanXuatID { get; set; }
        [DisplayName("Xuất xứ")]
        public string XuatXu { get; set; }
        [DisplayName("Bảo hành (tháng)")]
        public int BaoHanh { get; set; }
        [DataType(DataType.Date)]
        public DateTime NgayTao { get; set; }
        [DisplayName("Hiển thị trang chủ")]
        public bool HienThi { get; set; }
        [DisplayName("Sản phẩm bán chạy")]
        public bool SanPhamBanChay { get; set; }
        [DisplayName("Số lượng tồn")]
        public int SoLuong { get; set; }
        [DisplayName("Kích thước thùng")]
        public string KichThuocThung { get; set; }
        [DisplayName("Khối lượng thùng")]
        public string KhoiLuongThung { get; set; }
        [DisplayName("Mô tả chi tiết về sản phẩm")]
        public string MoTa { get; set; }
        // TIVI
        [DisplayName("Kích thước màn hình")]
        public string KichThuocMH { get; set; }
        [DisplayName("Độ phân giải")]
        public string DoPhanGiai { get; set; }
        [DisplayName("màn hình cong")]
        public string ManHinhCong { get; set; }
        [DisplayName("Bộ xử lý")]
        public string BoXuLy { get; set; }
        [DisplayName("SmartTV")]
        public string SmartTV { get; set; }
        [DisplayName("Tần số web")]
        public string TanSoQuet { get; set; }
        [DisplayName("Công suất loa")]
        public string CongSuatLoa { get; set; }
        [DisplayName("Cổng Wifi")]
        public string CongWiFi { get; set; }
        [DisplayName("Cổng Internet")]
        public string CongInternet { get; set; }
        [DisplayName("Cổng HDMI")]
        public string CongHDMI { get; set; }
        [DisplayName("Cổng USB")]
        public string CongUSB { get; set; }
        [DisplayName("Chia sẻ thông ming")]
        public string ChiaSeThongMinh { get; set; }
        [DisplayName("Hệ điều hành")]
        public string HeDeHanh { get; set; }
        [DisplayName("Trình duyệt web")]
        public string TrinhDuyetWeb { get; set; }
        // Dan may nghe nhac
        [DisplayName("Loại dàn máy")]
        public string LoaiDanMay { get; set; }
        [DisplayName("Loại đầu đĩa")]
        public string LoaiDauDia { get; set; }


        public LoaiSanPham LoaiSanPham { get; set; }
        public NhaSanXuat NhaSanXuat { get; set; }
        public List<ChiTietQuaTang> ChiTietQuaTang { get; set; }
        public List<BinhLuan> BinhLuans { get; set; }
        public List<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
