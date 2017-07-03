using EWareShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.ViewComponents
{
    public class TaiKhoanSideBarComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        { 
            var taiKhoan = HttpContext.Session.GetSession<EWareShop.Models.TaiKhoan>("DangNhapSession");
            return View(taiKhoan);
        }
    }
}
