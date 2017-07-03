using EWareShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWareShop.Component
{
    public class SliderAdvComponent: ViewComponent
    {
        private readonly EWareShopContext _context;

        public SliderAdvComponent(EWareShopContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dsctkm = from ctkm in _context.KhuyenMai
                         where ctkm.HienThi==true
                         select ctkm;
            return View(await dsctkm.ToListAsync());
        }
    }
}
