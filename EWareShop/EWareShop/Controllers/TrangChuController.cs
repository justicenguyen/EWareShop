using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EWareShop.Models;

namespace EWareShop.Controllers
{
    public class TrangChuController : Controller
    {
        private readonly EWareShopContext _context;

        public TrangChuController(EWareShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> TrangChu()
        {
            Microsoft.AspNetCore.Http.CookieOptions options = new Microsoft.AspNetCore.Http.CookieOptions();
            options.Expires = DateTime.Now.AddSeconds(30);

            var dssanphamhienthi = from spht in _context.SanPham
                                   where spht.HienThi == true
                                   select spht;
            var dssanphambanchay = from spbc in _context.SanPham
                                   where spbc.SanPhamBanChay == true
                                   select spbc;
            var dssanphammoinhat = (from spmn in _context.SanPham
                                    orderby spmn.NgayTao
                                    select spmn).Skip(0).Take(5);
            var dsloaisanpham = from lsp in _context.LoaiSanPham
                                select lsp;
            var dskhuyenmai = from km in _context.KhuyenMai
                              where km.HienThi == true
                              select km;
            ViewData["menu"] = dsloaisanpham;
            ViewData["dsloaisanpham"] = dsloaisanpham;
            var dsdulieu = new DuLieuTrangChu();
            dsdulieu.DSSanPhamHienThi = await dssanphamhienthi.ToListAsync();
            dsdulieu.DSLoaiSanPham = await dsloaisanpham.ToListAsync();
            dsdulieu.DSSanPhamBanChay = await dssanphambanchay.ToListAsync();
            dsdulieu.DSKhuyenMai = await dskhuyenmai.ToListAsync();
            return View(dsdulieu);
        }




        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
