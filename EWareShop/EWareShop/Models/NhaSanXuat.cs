using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class NhaSanXuat
    {
        public int NhaSanXuatID { get; set; }
        [DisplayName("Tên nhà sản xuất có dấu *")]
        [Required(ErrorMessage = "Trường này bắt buộc !")]
        public string TenNSXCoDau { get; set; }
        [DisplayName("Tên nhà sản xuất không dấu *")]
        [Required(ErrorMessage = "Trường này bắt buộc !")]
        public string TenNSXKhongDau { get; set; }

        public List<SanPham> SanPhams { get; set; }
    }
}
