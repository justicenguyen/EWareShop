using EWareShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.ViewComponents
{
    public class SanPhamAdminComponent : ViewComponent
    {
        private readonly EWareShopContext _context;

        public SanPhamAdminComponent(EWareShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int idSanPham)
        {
            var SanPham = await _context.SanPham.SingleOrDefaultAsync(p => p.SanPhamID == idSanPham);
            var danhSachQuaTang = from ctqt in _context.ChiTietQuaTang
                                  join qt in _context.QuaTang on ctqt.QuaTangID equals qt.QuaTangID
                                  where ctqt.SanPhamID == idSanPham
                                  select qt;
            var duLieuSanPhamAdmin = new DuLieuSanPhamAdmin();
            duLieuSanPhamAdmin.SanPham = SanPham;
            duLieuSanPhamAdmin.DanhSachQuaTangCuaSanPham = await danhSachQuaTang.ToListAsync();
            duLieuSanPhamAdmin.DanhSachTatCaQuaTang = await _context.QuaTang.ToListAsync();
            return View(duLieuSanPhamAdmin);
        }
    }
}
