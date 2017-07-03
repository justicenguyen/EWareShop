using EWareShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Component
{
    public class DSSanPhamKhuyenMaiComponent:ViewComponent
    {
        private readonly EWareShopContext _context;

        public DSSanPhamKhuyenMaiComponent(EWareShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int idLSP)
        {
            var dsSanPhamKhuyenMai = from sp in _context.SanPham
                                where sp.GiaGiam >0 && sp.LoaiSanPhamID==idLSP
                                select sp;
            return View(await dsSanPhamKhuyenMai.ToListAsync());
        }
    }
}
