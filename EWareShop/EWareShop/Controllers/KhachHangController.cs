using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EWareShop.Models;

namespace EWareShop.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly EWareShopContext _context;

        public KhachHangController(EWareShopContext context)
        {
            _context = context;
        }

        // GET: KhachHang
        public async Task<IActionResult> Index()
        {
            return View(await _context.KhachHang.ToListAsync());
        }
        //Ajax thêm khách hàng
        public IActionResult ThemKhachHang(KhachHang khachHang)
        {
            string khachHangId = "0";
            if (ModelState.IsValid)
            {
                var khachhang = (from kh in _context.KhachHang
                                 where kh.SoDienThoai == khachHang.SoDienThoai
                                 select kh).FirstOrDefault();

                if (khachhang == null)
                {
                    _context.Add(khachHang);
                    _context.SaveChanges();
                    var khachhanghientai = (from kh in _context.KhachHang
                                            orderby kh.KhachHangID descending
                                            select kh).FirstOrDefault();
                    khachHangId = Convert.ToString(khachhanghientai.KhachHangID);
                }
                else
                {
                    khachHangId = Convert.ToString(khachhang.KhachHangID);
                }
            }
            return Content(khachHangId);
        }

        // GET: KhachHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var khachHang = await _context.KhachHang
                .SingleOrDefaultAsync(m => m.KhachHangID == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // GET: KhachHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HoTenKhachHang,SoDienThoai,DiaChi,Email")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                if (_context.KhachHang.Any(e => e.SoDienThoai == khachHang.SoDienThoai))
                {
                    var khachHangHienTai = (from kh in _context.KhachHang
                                            where kh.SoDienThoai == khachHang.SoDienThoai
                                            select kh).FirstOrDefault();
                    HttpContext.Session.SetSession("KhachHangHienTai", khachHangHienTai);
                    HttpContext.Response.Cookies.SetKookies("KhachHangHienTai", khachHangHienTai);
                }
                else
                {
                    _context.Add(khachHang);
                    await _context.SaveChangesAsync();
                    var khachHangHienTai = (from kh in _context.KhachHang
                                            orderby kh.KhachHangID descending
                                            select kh).FirstOrDefault();
                    HttpContext.Session.SetSession("KhachHangHienTai", khachHangHienTai);
                    HttpContext.Response.Cookies.SetKookies("KhachHangHienTai", khachHangHienTai);
                }
                return RedirectToAction("BanHang1", "SanPhamBan");
            }
            return View(khachHang);
        }

        // GET: KhachHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            var danhSachSanPhamDaMua = from spb in _context.SanPhamBan
                                       where spb.KhachHangID == id
                                       select spb;
            ViewBag.danhSachSanPhamDaMua = await danhSachSanPhamDaMua.ToListAsync();
            var khachHang = await _context.KhachHang.SingleOrDefaultAsync(m => m.KhachHangID == id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,HoTenKhachHang,SoDienThoai,DiaChi,Email")] KhachHang khachHang)
        {
            if (id != khachHang.KhachHangID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachHangExists(khachHang.KhachHangID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            var danhSachSanPhamDaMua = from spb in _context.SanPhamBan
                                       where spb.KhachHangID == id
                                       select spb;
            ViewBag.danhSachSanPhamDaMua = await danhSachSanPhamDaMua.ToListAsync();
            return View(khachHang);
        }

        // GET: KhachHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHang
                .SingleOrDefaultAsync(m => m.KhachHangID == id);
            _context.KhachHang.Remove(khachHang);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khachHang = await _context.KhachHang.SingleOrDefaultAsync(m => m.KhachHangID == id);
            _context.KhachHang.Remove(khachHang);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool KhachHangExists(int id)
        {
            return _context.KhachHang.Any(e => e.KhachHangID == id);
        }
        //Ajax tìm kiếm khách hàng
        public IActionResult TimKiemKhachHang(string soDienThoai)
        {
            var ketqua = "0";
            var khachHang = (from kh in _context.KhachHang
                             where kh.SoDienThoai == soDienThoai
                             select kh).FirstOrDefault();
            if (khachHang != null)
            {
                ketqua = "1";
            }
            return Content(ketqua);
        }
        public IActionResult ChuyenTiep(string sodienthoai)
        {
            var khachHangHienTai = (from kh in _context.KhachHang
                                    where kh.SoDienThoai == sodienthoai
                                    select kh).FirstOrDefault();
            HttpContext.Session.SetSession("KhachHangHienTai", khachHangHienTai);
            HttpContext.Response.Cookies.SetKookies("KhachHangHienTai", khachHangHienTai);
            return RedirectToAction("BanHang1", "SanPhamBan");
        }

        //public async Task<IActionResult> DanhSachSanPhamDaMua(int idKhachHang)
        //{
        //    var thongTinKhachHang = (from kh in _context.KhachHang
        //                            where kh.ID == idKhachHang
        //                            select kh).FirstOrDefault();
        //    var danhSachSanPhamDaMu = from spb in _context.SanPhamBan
        //                              where spb.khachHangID == idKhachHang
        //                              select spb;
        //    var duLieuDanhSachSanPhamDaMua = new DuLieuDanhSachSanPhamDaMua();
        //    duLieuDanhSachSanPhamDaMua.thongTinKhachHang = thongTinKhachHang;
        //    duLieuDanhSachSanPhamDaMua.danhSachSanPhamDaMua = await danhSachSanPhamDaMu.ToListAsync();
        //    return View(duLieuDanhSachSanPhamDaMua);
        //}
    }
}