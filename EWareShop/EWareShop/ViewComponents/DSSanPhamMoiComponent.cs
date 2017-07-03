using EWareShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Component
{
    public class DSSanPhamMoiComponent:ViewComponent
    {
        private readonly EWareShopContext _context;

        public DSSanPhamMoiComponent(EWareShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dsspmoi = (from lsp in _context.SanPham
                           orderby lsp.SanPhamID descending
                           select lsp).Skip(0).Take(6);
            return View(await dsspmoi.ToListAsync());
        }
    }
}
