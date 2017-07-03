using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
//using System.Web.Script.Seriallization;
using Newtonsoft.Json;
using EWareShop.Models;

namespace EWareShop.Controllers
{
    public class GioHangController : Controller
    {
        private readonly EWareShopContext _context;
        public GioHangController(EWareShopContext context)
        {
            _context = context;
        }
        private const string GioHangSession = "GioHangSession";
        public IActionResult GioHang()
        {
           
            var cart = HttpContext.Session.GetSession<List<SanPhamGioHang>>(GioHangSession);
            if (cart == null)
            {
                cart = HttpContext.Request.Cookies.GetKookies<List<SanPhamGioHang>>(GioHangSession);
            }
            var list = new List<SanPhamGioHang>();
            if (cart != null)
            {
                list = (List<SanPhamGioHang>)cart;
            }
            return View(list);
        }

        public IActionResult GioHang1()
        {

            var cart = HttpContext.Session.GetSession<List<SanPhamGioHang>>(GioHangSession);
            if (cart == null)
            {
                cart = HttpContext.Request.Cookies.GetKookies<List<SanPhamGioHang>>(GioHangSession);
            }
            var list = new List<SanPhamGioHang>();
            if (cart != null)
            {
                list = (List<SanPhamGioHang>)cart;
            }
            return View(list);
        }

        public async Task<IActionResult> ThemGioHang(int idSP, int soLuong)
        {
            var sanPham = await _context.SanPham
               .SingleOrDefaultAsync(m => m.SanPhamID == idSP);

            var cart = HttpContext.Session.GetSession<List<SanPhamGioHang>>(GioHangSession);
            if (cart == null)
            {
                cart = HttpContext.Request.Cookies.GetKookies<List<SanPhamGioHang>>(GioHangSession);
            }
            if (cart != null)
            {
                var list = (List<SanPhamGioHang>)cart;
                if (list.Exists(x => x.SanPhamGioHangID == idSP))
                {
                    foreach (var item in list)
                    {
                        if (item.SanPhamGioHangID == idSP)
                        {
                            item.SoLuong += soLuong;
                            int GiaThem = soLuong * item.Gia;
                            item.TongTien = item.TongTien + GiaThem;
                        }
                    }
                }
                else
                {
                    var item = new SanPhamGioHang();
                    item.SanPhamGioHangID = sanPham.SanPhamID;
                    item.TenSP = sanPham.TenSPCoDau;
                    item.Gia = sanPham.GiaBan;
                    item.HinhAnh = sanPham.HinhAnh;
                    item.SoLuong = soLuong;
                    item.TongTien = soLuong * sanPham.GiaBan;
                    list.Add(item);
                }
                //Gan vao session
                HttpContext.Session.SetSession(GioHangSession, list);
                HttpContext.Response.Cookies.SetKookies(GioHangSession, list);

            }
            else
            {
                var item = new SanPhamGioHang();
                item.SanPhamGioHangID = sanPham.SanPhamID;
                item.TenSP = sanPham.TenSPCoDau;
                item.Gia = sanPham.GiaBan;
                item.HinhAnh = sanPham.HinhAnh;
                item.SoLuong = soLuong;
                item.TongTien = soLuong * sanPham.GiaBan;
                var list = new List<SanPhamGioHang>();
                list.Add(item);
                //Gan vao session
                HttpContext.Session.SetSession(GioHangSession, list);
                HttpContext.Response.Cookies.SetKookies(GioHangSession, list);
            }
            return Content("The vao gio hang thanh cong", "text/plain");
        }

        // Ajax cập nhật giỏ hàng
        public async Task<IActionResult> SuaGioHang(int idSP, int soLuong)
        {
            var sanPham = await _context.SanPham
               .SingleOrDefaultAsync(m => m.SanPhamID == idSP);
            var cart = HttpContext.Session.GetSession<List<SanPhamGioHang>>(GioHangSession);
            if (cart == null)
            {
                cart = HttpContext.Request.Cookies.GetKookies<List<SanPhamGioHang>>(GioHangSession);
            }
            if (cart != null)
            {
                var list = (List<SanPhamGioHang>)cart;
                if (list.Exists(x => x.SanPhamGioHangID == idSP))
                {
                    foreach (var item in list)
                    {
                        if (item.SanPhamGioHangID == idSP)
                        {
                            item.SoLuong += soLuong;
                            int GiaThem = soLuong * item.Gia;
                            item.TongTien = item.TongTien + GiaThem;
                        }
                    }
                }
                else
                {
                    var item = new SanPhamGioHang();
                    item.SanPhamGioHangID = sanPham.SanPhamID;
                    item.TenSP = sanPham.TenSPCoDau;
                    item.Gia = sanPham.GiaBan;
                    item.HinhAnh = sanPham.HinhAnh;
                    item.SoLuong = soLuong;
                    item.TongTien = soLuong * sanPham.GiaBan;
                    list.Add(item);
                }
                //Gan vao session
                HttpContext.Session.SetSession(GioHangSession, list);
                HttpContext.Response.Cookies.SetKookies(GioHangSession, list);

            }
            else
            {
                var item = new SanPhamGioHang();
                item.SanPhamGioHangID = sanPham.SanPhamID;
                item.TenSP = sanPham.TenSPCoDau;
                item.Gia = sanPham.GiaBan;
                item.SoLuong = soLuong;
                item.HinhAnh = sanPham.HinhAnh;
                item.TongTien = soLuong * sanPham.GiaBan;
                var list = new List<SanPhamGioHang>();
                list.Add(item);
                //Gan vao session
                HttpContext.Session.SetSession(GioHangSession, list);
                HttpContext.Response.Cookies.SetKookies(GioHangSession, list);
            }
            int tongtien = 0;
            foreach(var spgh in cart)
            {
                tongtien += spgh.TongTien;
            }
            return Content(""+ tongtien, "text/plain");
        }
        // Xóa sản phẩm trong giỏ hàng
        public IActionResult XoaGioHang(int idSP)
        {
            var cart = HttpContext.Session.GetSession<List<SanPhamGioHang>>(GioHangSession);
            cart.RemoveAll(x => x.SanPhamGioHangID == idSP);
            HttpContext.Session.SetSession(GioHangSession, cart);
            HttpContext.Response.Cookies.SetKookies(GioHangSession, cart);
            int tongtien = 0;
            foreach (var spgh in cart)
            {
                tongtien += spgh.TongTien;
            }
            return Content("" + tongtien, "text/plain");
        }
        //Đặt hàng
        public async Task<IActionResult> DatHang()
        {

            var dsPhiGiaoHang = from pgh in _context.PhiGiaoHang
                                select pgh;
            var gioHang = HttpContext.Session.GetSession<List<SanPhamGioHang>>(GioHangSession);
            if(gioHang==null)
            {
                return RedirectToAction("GioHang1");
            }
            ViewBag.dsPhiGiaoHang = await dsPhiGiaoHang.ToListAsync();
            ViewBag.gioHang = gioHang;
            return View();
        }
        public async Task<IActionResult> DatHang1([Bind("TenKhachHang,SoDienThoai,Email,DiaChi")] DonHang donHang)
        {
            //DonHang dh = new DonHang { TenKhachHang = donHang.TenKhachHang, SoDienThoai = donHang.SoDienThoai, Email = donHang.Email, DiaChi = donHang.DiaChi, TongTien = 1111 };
            //_context.DonHang.Add(dh);
            if (ModelState.IsValid)
            {

                var cart = HttpContext.Session.GetSession<List<SanPhamGioHang>>(GioHangSession);
                int TongTien = 0;
                foreach(var tt in cart)
                {
                    TongTien = TongTien + tt.TongTien;
                }
                donHang.TongTien = TongTien;
                _context.Add(donHang);
                await _context.SaveChangesAsync();
                var dh = (from d in _context.DonHang
                          orderby d.DonHangID descending
                          select d).FirstOrDefault();
               
                foreach(var sp in cart)
                {
                    ChiTietDonHang ctdh = new ChiTietDonHang { SanPhamID=sp.SanPhamGioHangID,DonHangID= dh.DonHangID,Gia=sp.Gia,SoLuong=sp.SoLuong,TongTien=sp.TongTien};
                    _context.ChiTietDonHang.Add(ctdh);
                }
                await _context.SaveChangesAsync();
                HttpContext.Session.SetSession(GioHangSession, null);
                HttpContext.Response.Cookies.SetKookies(GioHangSession, null);
                return RedirectToAction("ThongBaoDaNhanDatHang");
            }
            return RedirectToAction("GioHang");
        }

        public IActionResult ThongBaoDaNhanDatHang()
        {
            return View();
        }
        //Ajax lấy phí giao hàng
        public IActionResult LayPhiGiaoHang(int idPGH)
        {
            var phiGiaoHang = (from pgh in _context.PhiGiaoHang
                              where pgh.PhiGiaoHangID == idPGH
                              select pgh).FirstOrDefault();
            return Content(""+phiGiaoHang.TienPhi,"text/plain");
        }

        //Đặt hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DatHang([Bind("TenKhachHang,SoDienThoai,Email,TinhThanhPho,DiaChi,GhiChu")]DonHang donHang)
        {
            if(ModelState.IsValid)
            {
                var cart = HttpContext.Session.GetSession<List<SanPhamGioHang>>(GioHangSession);
                var phiGiaoHang = (from pgh in _context.PhiGiaoHang
                                  where pgh.PhiGiaoHangID == donHang.TinhThanhPho
                                  select pgh).FirstOrDefault();
                int TongTien = 0;
                foreach (var tt in cart)
                {
                    TongTien = TongTien + tt.TongTien;
                }
                if(TongTien<=10000000)
                {
                    TongTien += phiGiaoHang.TienPhi;
                }
                var ngayDat = DateTime.Now;
                donHang.NgayDat = ngayDat;
                donHang.TongTien = TongTien;
                _context.Add(donHang);
                await _context.SaveChangesAsync();
                //Lấy thông tin đơn hàng vừa mới thêm
                var dh = (from d in _context.DonHang
                          orderby d.DonHangID descending
                          select d).FirstOrDefault();
                foreach (var sp in cart)
                {
                    ChiTietDonHang ctdh = new ChiTietDonHang { SanPhamID = sp.SanPhamGioHangID, DonHangID = dh.DonHangID, Gia = sp.Gia, SoLuong = sp.SoLuong, TongTien = sp.TongTien };
                    _context.ChiTietDonHang.Add(ctdh);
                }
                await _context.SaveChangesAsync();
                HttpContext.Session.SetSession(GioHangSession, null);
                HttpContext.Response.Cookies.SetKookies(GioHangSession, null);
                return RedirectToAction("ThongBaoDaNhanDatHang");
            }
            var dsPhiGiaoHang = from pgh in _context.PhiGiaoHang
                                select pgh;
            var gioHang = HttpContext.Session.GetSession<List<SanPhamGioHang>>(GioHangSession);
            ViewBag.dsPhiGiaoHang = await dsPhiGiaoHang.ToListAsync();
            ViewBag.gioHang = gioHang;
            return View(donHang);
        }
    }
}