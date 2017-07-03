using EWareShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Component
{
    public class BoLocComponent:ViewComponent
    {
        private readonly EWareShopContext _context;

        public BoLocComponent(EWareShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int a, int b)
        {
            var dsHangSanXuat = from nsx in _context.NhaSanXuat
                                where nsx.NhaSanXuatID > a  
                              select nsx;
            return View(await dsHangSanXuat.ToListAsync());
        }

    }
}
