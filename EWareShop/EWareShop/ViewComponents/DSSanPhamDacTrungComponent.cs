using EWareShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Component
{
    public class DSSanPhamDacTrungComponent:ViewComponent
    {
        private readonly EWareShopContext _context;

        public DSSanPhamDacTrungComponent(EWareShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            var dsSanPhamBanChay = from sp in _context.SanPham
                                   where sp.HienThi == true
                                   select sp;
            return View(await dsSanPhamBanChay.ToListAsync());
        }
    }
}
