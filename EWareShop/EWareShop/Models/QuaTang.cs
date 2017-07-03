using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Models
{
    public class QuaTang
    {
        [Key]
        public int QuaTangID { get; set; }
        [Required(ErrorMessage ="Trường này là bắt buộc")]
        public string TenQuaTang { get; set; }
        
        public List<ChiTietQuaTang> ChiTietQuaTangs { get; set; }
    }
}
