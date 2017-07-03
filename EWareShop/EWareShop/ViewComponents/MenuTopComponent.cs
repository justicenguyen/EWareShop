using EWareShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Component
{
    public class MenuTopComponent:ViewComponent
    {
        private readonly EWareShopContext _context;

        public MenuTopComponent(EWareShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dsloaisp = from lsp in _context.LoaiSanPham
                           select lsp;
            return View(await dsloaisp.ToListAsync());
        }
    }
}
