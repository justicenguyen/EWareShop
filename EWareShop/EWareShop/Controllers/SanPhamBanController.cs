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
    public class SanPhamBanController : Controller
    {
        private readonly EWareShopContext _context;
        private const string BanHangSession = "BanHangSession";

        public SanPhamBanController(EWareShopContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> BanHang()
        {
            var dsLoaiSanPham = from lsp in _context.LoaiSanPham
                                select lsp;
            var loaiSanPhamDauTien = await (from lsp in dsLoaiSanPham
                                            select lsp).FirstOrDefaultAsync();
            var dsSanPhamTheoLoai = from sp in _context.SanPham
                                    where sp.LoaiSanPhamID == loaiSanPhamDauTien.LoaiSanPhamID
                                    select sp;
            ViewBag.dsLoaiSanPham = await dsLoaiSanPham.ToListAsync();
            ViewBag.dsSanPhamTheoLoai = await dsSanPhamTheoLoai.ToListAsync();
            return View();
        }
        public async Task<IActionResult> BanHang1(string loaisanpham, string tukhoa)
        {
            var thongTinKhachHangSession = HttpContext.Session.GetSession<KhachHang>("KhachHangHienTai");
            if (thongTinKhachHangSession == null)
            {
                thongTinKhachHangSession = HttpContext.Request.Cookies.GetKookies<KhachHang>("KhachHangHienTai");
            }
            var thongTinKhachHang = new KhachHang();
            if (thongTinKhachHangSession != null)
            {
                thongTinKhachHang = (KhachHang)thongTinKhachHangSession;
            }
            ViewBag.khachHangHienTai = thongTinKhachHang;
            // Lấy các sản phẩm đã chọn bán
            var cart = HttpContext.Session.GetSession<List<SanPhamChon>>(BanHangSession);
            if (cart == null)
            {
                cart = HttpContext.Request.Cookies.GetKookies<List<SanPhamChon>>(BanHangSession);
            }
            var dsSP = new List<SanPhamChon>();
            if (cart != null)
            {
                dsSP = (List<SanPhamChon>)cart;
            }
            ViewBag.dsSanPhamChonBan = dsSP;

            var dsLoaiSanPham = from lsp in _context.LoaiSanPham
                                select lsp;
            var dsSanPham = from sp in _context.SanPham
                            select sp;
            ViewBag.LoaiSanPham = "";
            ViewBag.TuKhoa = "";
            if (String.IsNullOrEmpty(loaisanpham))
            {
                dsSanPham = from sp in dsSanPham
                            where sp.LoaiSanPhamID == dsLoaiSanPham.FirstOrDefault().LoaiSanPhamID
                            select sp;
            }
            else
            {
                ViewBag.LoaiSanPham = loaisanpham;
                var LoaiSanPham = (from lsp in _context.LoaiSanPham
                                   where lsp.TenLoaiSPKhongDau == loaisanpham
                                   select lsp).FirstOrDefault();
                if (String.IsNullOrEmpty(tukhoa))
                {
                    dsSanPham = from sp in dsSanPham
                                where sp.LoaiSanPhamID == LoaiSanPham.LoaiSanPhamID
                                select sp;
                }
                else
                {
                    dsSanPham = from sp in dsSanPham
                                where sp.LoaiSanPhamID == LoaiSanPham.LoaiSanPhamID && sp.TenSPCoDau.Contains(tukhoa)
                                select sp;
                    ViewBag.TuKhoa = tukhoa;
                }

            }

            var duLieuBanHang = new DuLieuBanHang();
            duLieuBanHang.DSLoaiSanPham = await dsLoaiSanPham.ToListAsync();
            duLieuBanHang.DSSanPham = await dsSanPham.ToListAsync();
            return View(duLieuBanHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BanHang([Bind("ID,sanPhamID,khachHangID,maBaoHanh")] SanPhamBan SanPhamBan, string TenSP)
        {
            if (ModelState.IsValid)
            {
                var ngayTao = DateTime.Now;
                SanPhamBan.NgayBan = ngayTao;
                _context.Add(SanPhamBan);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var khachhang = (from kh in _context.KhachHang
                             where kh.KhachHangID == SanPhamBan.KhachHangID
                             select kh).FirstOrDefault();
            if (khachhang != null)
            {
                ViewBag.tenKH = khachhang.HoTenKhachHang;
                ViewBag.soDT = khachhang.SoDienThoai;
                ViewBag.diaChi = khachhang.DiaChi;
            }
            ViewBag.tenSP = TenSP;
            ViewBag.sanPhamID = SanPhamBan.SanPhamBanID;
            var dsLoaiSanPham = from lsp in _context.LoaiSanPham
                                select lsp;
            var loaiSanPhamDauTien = await (from lsp in dsLoaiSanPham
                                            select lsp).FirstOrDefaultAsync();
            var dsSanPhamTheoLoai = from sp in _context.SanPham
                                    where sp.LoaiSanPhamID == loaiSanPhamDauTien.LoaiSanPhamID
                                    select sp;
            ViewBag.dsLoaiSanPham = await dsLoaiSanPham.ToListAsync();
            ViewBag.dsSanPhamTheoLoai = await dsSanPhamTheoLoai.ToListAsync();
            return View(SanPhamBan);
        }
        //Ajax load danh sách sản phẩm theo loại
        public IActionResult LoadSanPhamTheoLoai(int idlsp)
        {
            var dsSanPhamTheoLoai = from sp in _context.SanPham
                                    where sp.LoaiSanPhamID == idlsp
                                    select sp;
            var rs = "";
            foreach (var sp in dsSanPhamTheoLoai)
            {
                rs = rs + "<option value= '" + sp.SanPhamID + "'>" + sp.TenSPCoDau + "</option>";
            }
            return Content(rs);
        }

        //Ajax load danh sách sản phẩm theo loại
        public IActionResult LoadThongTinSanPham(int idsp)
        {
            var sanPham = (from sp in _context.SanPham
                           where sp.SanPhamID == idsp
                           select sp).FirstOrDefault();
            var rs = "<div class='col-sm-5'>" +
                    "<img src='/" + sanPham.HinhAnh + "' class='img-rounded'  width='150' height='150'>" +
            "</div>" +
            "<div class='col-sm-7'>" +
                "<p id='tensp'><b>" + sanPham.TenSPCoDau + "</b></p>" +
                "<p>Giá gốc :" + sanPham.GiaGoc + "</p>" +
                "<p>Giá giảm :" + sanPham.GiaGiam + "</p>" +
            "</div>" +
            "<input type='hidden' id ='masp' value='" + sanPham.SanPhamID + "' />";

            return Content(rs);
        }

        //Ajax load danh sách sản phẩm theo loại
        public IActionResult LoadThongTinSanPhamDauTien(int idlsp)
        {
            var dsSanPhamTheoLoai = (from sp in _context.SanPham
                                     where sp.LoaiSanPhamID == idlsp
                                     select sp).FirstOrDefault();
            var sanPham = (from sp in _context.SanPham
                           where sp.SanPhamID == dsSanPhamTheoLoai.SanPhamID
                           select sp).FirstOrDefault();
            var rs = "<div class='col-sm-5'>" +
                    "<img src='/" + sanPham.HinhAnh + "' class='img-rounded'  width='150' height='150'>" +
            "</div>" +
            "<div class='col-sm-7'>" +
                "<p id='tensp'><b>" + sanPham.TenSPCoDau + "</b></p>" +
                "<p>Giá gốc :" + sanPham.GiaGoc + "</p>" +
                "<p>Giá giảm :" + sanPham.GiaGiam + "</p>" +
            "</div>" +
            "<input type='hidden' id ='masp' value='" + sanPham.SanPhamID + "' />";
            return Content(rs);
        }

        // GET: SanPhamBan
        public async Task<IActionResult> Index()
        {
            return View(await _context.SanPhamBan.ToListAsync());
        }

        // GET: SanPhamBan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SanPhamBan = await _context.SanPhamBan
                .SingleOrDefaultAsync(m => m.SanPhamBanID == id);
            if (SanPhamBan == null)
            {
                return NotFound();
            }

            return View(SanPhamBan);
        }

        // GET: SanPhamBan/Create
        public IActionResult Create()
        {
            var dssp = from spb in _context.SanPhamBan
                       select spb;
            ViewBag.dsLoaiSanPham = dssp.ToList();
            return View();
        }
        // POST: SanPhamBan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,sanPhamID,khachHangID,maBaoHanh")] SanPhamBan SanPhamBan)
        {
            var dssp = from spb in _context.SanPhamBan
                       select spb;
            ViewBag.dsLoaiSanPham = dssp.ToList();
            if (ModelState.IsValid)
            {
                _context.Add(SanPhamBan);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(SanPhamBan);
        }

        // GET: SanPhamBan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SanPhamBan = await _context.SanPhamBan.SingleOrDefaultAsync(m => m.SanPhamBanID == id);
            if (SanPhamBan == null)
            {
                return NotFound();
            }
            return View(SanPhamBan);
        }

        // POST: SanPhamBan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,sanPhamID,maBaoHanh,ngayBan")] SanPhamBan SanPhamBan)
        {
            if (id != SanPhamBan.SanPhamBanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(SanPhamBan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamBanExists(SanPhamBan.SanPhamBanID))
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
            return View(SanPhamBan);
        }

        // GET: SanPhamBan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SanPhamBan = await _context.SanPhamBan
                .SingleOrDefaultAsync(m => m.SanPhamBanID == id);
            if (SanPhamBan == null)
            {
                return NotFound();
            }

            return View(SanPhamBan);
        }

        // POST: SanPhamBan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var SanPhamBan = await _context.SanPhamBan.SingleOrDefaultAsync(m => m.SanPhamBanID == id);
            _context.SanPhamBan.Remove(SanPhamBan);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SanPhamBanExists(int id)
        {
            return _context.SanPhamBan.Any(e => e.SanPhamBanID == id);
        }

        //Chọn sản phẩm bán
        public IActionResult ChonSanPham(string idSanPham, string mabaoHanh)
        {
            var cart = HttpContext.Session.GetSession<List<SanPhamChon>>(BanHangSession);
            if (cart == null)
            {
                cart = HttpContext.Request.Cookies.GetKookies<List<SanPhamChon>>(BanHangSession);
            }
            if (cart != null)
            {
                var sanPhamChonBan = (from sp in _context.SanPham
                                      where sp.SanPhamID == int.Parse(idSanPham)
                                      select sp).FirstOrDefault();
                var list = (List<SanPhamChon>)cart;
                var item = new SanPhamChon();
                item.SanPham = sanPhamChonBan;
                item.MaBaoHanh = mabaoHanh;
                list.Add(item);
                //Gan vao session
                HttpContext.Session.SetSession(BanHangSession, list);
                HttpContext.Response.Cookies.SetKookies(BanHangSession, list);

            }
            else
            {
                var sanPhamChonBan = (from sp in _context.SanPham
                                      where sp.SanPhamID == int.Parse(idSanPham)
                                      select sp).FirstOrDefault();
                var item = new SanPhamChon();
                item.SanPham = sanPhamChonBan;
                item.MaBaoHanh = mabaoHanh;
                var list = new List<SanPhamChon>();
                list.Add(item);
                //Gan vao session
                HttpContext.Session.SetSession(BanHangSession, list);
                HttpContext.Response.Cookies.SetKookies(BanHangSession, list);
            }
            return RedirectToAction("BanHang1");
        }
        public IActionResult XoaSanPhamChon(string id)
        {
            var cart = HttpContext.Session.GetSession<List<SanPhamChon>>(BanHangSession);
            cart.RemoveAll(x => x.MaBaoHanh == id);
            HttpContext.Session.SetSession(BanHangSession, cart);
            HttpContext.Response.Cookies.SetKookies(BanHangSession, cart);
            return RedirectToAction("BanHang1");
        }
        public async Task<IActionResult> XacNhanBan()
        {
            var cart = HttpContext.Session.GetSession<List<SanPhamChon>>(BanHangSession);
            if (cart == null)
            {
                cart = HttpContext.Request.Cookies.GetKookies<List<SanPhamChon>>(BanHangSession);
            }
            var thongTinKhachHangSession = HttpContext.Session.GetSession<KhachHang>("KhachHangHienTai");
            if (thongTinKhachHangSession == null)
            {
                thongTinKhachHangSession = HttpContext.Request.Cookies.GetKookies<KhachHang>("KhachHangHienTai");
            }
            var ngayBan = DateTime.Now;
            foreach (var spb in cart)
            {
                
                var sanPhamBan = new SanPhamBan();
                sanPhamBan.KhachHangID = thongTinKhachHangSession.KhachHangID;
                sanPhamBan.SanPhamID = spb.SanPham.SanPhamID;
                sanPhamBan.TenSanPham = spb.SanPham.TenSPCoDau;
                sanPhamBan.SoThangBaoHanh = spb.SanPham.BaoHanh;
                sanPhamBan.NgayHetBaoHanh = ngayBan.AddMonths(spb.SanPham.BaoHanh);
                sanPhamBan.MaBaoHanh = spb.MaBaoHanh;
                sanPhamBan.NgayBan = ngayBan;
                _context.SanPhamBan.Add(sanPhamBan);
                await _context.SaveChangesAsync();
            }
            HttpContext.Session.SetSession(BanHangSession, null);
            HttpContext.Response.Cookies.SetKookies(BanHangSession, null);
            HttpContext.Session.SetSession("KhachHangHienTai", null);
            HttpContext.Response.Cookies.SetKookies("KhachHangHienTai", null);
            return View("BanHang1");
        }
        //Ajax kiểm tra mã bảo hành
        public IActionResult KiemTraMaBaoHanh(string maBaoHanh)
        {
            var danhSachSanPhamDaBan = from spdb in _context.SanPhamBan
                                       select spdb;
            var danhSachSanPhamDaChon = HttpContext.Session.GetSession<List<SanPhamChon>>(BanHangSession);
            if (danhSachSanPhamDaChon == null)
            {
                danhSachSanPhamDaChon = HttpContext.Request.Cookies.GetKookies<List<SanPhamChon>>(BanHangSession);
            }
            var ketqua = "1";
            foreach (var spdb in danhSachSanPhamDaBan)
            {
                if (maBaoHanh == spdb.MaBaoHanh)
                {
                    ketqua = "0";
                    break;
                }
            }

            if (danhSachSanPhamDaChon != null)
            {
                foreach (var spdc in danhSachSanPhamDaChon)
                {
                    if (maBaoHanh == spdc.MaBaoHanh)
                    {
                        ketqua = "0";
                        break;
                    }
                }
            }

            return Content(ketqua);
        }
    }
}
