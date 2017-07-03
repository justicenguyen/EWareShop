using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class PhiGiaoHang
    {
        public int PhiGiaoHangID { get; set; }
        [DisplayName("Địa điểm *")]
        [Required(ErrorMessage = "Trường này là bắt buộc !")]
        public string DiaDiem { get; set; }
        [DisplayName("Phí giao hàng*")]
        [Required(ErrorMessage = "Trường này là bắt buộc !")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public int TienPhi{ get; set;}
    }
}
