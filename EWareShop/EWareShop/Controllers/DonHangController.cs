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
    public class DonHangController : Controller
    {
        private readonly EWareShopContext _context;

        public DonHangController(EWareShopContext context)
        {
            _context = context;
        }

        // GET: DonHang
        public async Task<IActionResult> Index(string daduyet,string dagiao,string trang)
        {
            var duLieuDonHangIndex = new DuLieuDonHangIndex();
            var dsDonHang = from dh in _context.DonHang
                            select dh;
            duLieuDonHangIndex.DaDuyet = "tatca";
            duLieuDonHangIndex.DaGiao = "tatca";
            if (!String.IsNullOrEmpty(daduyet))
            {
                if(!daduyet.Equals("tatca"))
                {
                    if (daduyet.Equals("true"))
                    {
                        dsDonHang = from dh2 in dsDonHang
                                    where dh2.DaDuyet == true
                                    select dh2;
                        duLieuDonHangIndex.DaDuyet = daduyet;
                    }
                    if (daduyet.Equals("false"))
                    {
                        dsDonHang = from dh2 in dsDonHang
                                    where dh2.DaDuyet == false
                                    select dh2;
                        duLieuDonHangIndex.DaDuyet = daduyet;
                    }
                }
            }
            if (!String.IsNullOrEmpty(dagiao))
            {
                if (!dagiao.Equals("tatca"))
                {
                    if(dagiao.Equals("true"))
                    {
                        dsDonHang = from dh2 in dsDonHang
                                    where dh2.DaGiao == true
                                    select dh2;
                        duLieuDonHangIndex.DaGiao = dagiao;
                    }
                    if (dagiao      .Equals("false"))
                    {
                        dsDonHang = from dh2 in dsDonHang
                                    where dh2.DaGiao == false
                                    select dh2;
                        duLieuDonHangIndex.DaGiao = dagiao;
                    }
                }
            }
            //Phân trang
            int trangHienTai = 1;
            if (!String.IsNullOrEmpty(trang))
            {
                trangHienTai = int.Parse(trang);
            }
            int soSanPhamTrenTrang = 2;
            int soTrangSanPham = 0;
            if (dsDonHang.Count() % soSanPhamTrenTrang == 0)
            {
                soTrangSanPham = dsDonHang.Count() / soSanPhamTrenTrang;
            }
            else
            {
                soTrangSanPham = dsDonHang.Count() / soSanPhamTrenTrang + 1;
            }
            int viTri = (trangHienTai * soSanPhamTrenTrang - soSanPhamTrenTrang);
            dsDonHang = (from sp in dsDonHang
                         orderby sp.DonHangID descending
                         select sp).Skip(viTri).Take(soSanPhamTrenTrang);

            duLieuDonHangIndex.DSDonHang = await dsDonHang.ToListAsync();
            duLieuDonHangIndex.SoTrangSanPham = soTrangSanPham;
            duLieuDonHangIndex.TrangHienTai = trangHienTai;

            return View(duLieuDonHangIndex);
        }


        public async Task<IActionResult> ChiTietDonHang(int? id)
        {
            var donHang = await _context.DonHang
                .SingleOrDefaultAsync(m => m.DonHangID == id);
            var dsChiTiet = from ct in _context.ChiTietDonHang
                            where ct.DonHangID == id
                            select ct;
            var phiShip = await _context.PhiGiaoHang.SingleOrDefaultAsync(pgh => pgh.PhiGiaoHangID == donHang.TinhThanhPho);
            ViewBag.phiGiaoHang = phiShip;
            var dsMatHang = new List<MatHang>();
            foreach (var ct in dsChiTiet)
            {
                var sanPham = await _context.SanPham.SingleOrDefaultAsync(sp => sp.SanPhamID == ct.SanPhamID);
                var mh = new MatHang();
                mh.SanPham = sanPham;
                mh.SoLuong = ct.SoLuong;
                mh.TongTien = ct.TongTien;
                dsMatHang.Add(mh);
            }
            var duLieuChiTiet = new DuLieuChiTietDonHang();
            duLieuChiTiet.DonHang = donHang;
            duLieuChiTiet.ChiTietDonHang = dsMatHang;
            return View(duLieuChiTiet);
        }

        // GET: DonHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHang
                .SingleOrDefaultAsync(m => m.DonHangID == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // GET: DonHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DonHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TenKhachHang,SoDienThoai,Email,DiaChi,TongTien")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donHang);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(donHang);
        }

        // GET: DonHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHang.SingleOrDefaultAsync(m => m.DonHangID == id);
            if (donHang == null)
            {
                return NotFound();
            }
            return View(donHang);
        }

        // POST: DonHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TenKhachHang,SoDienThoai,Email,DiaChi,TongTien")] DonHang donHang)
        {
            if (id != donHang.DonHangID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonHangExists(donHang.DonHangID))
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
            return View(donHang);
        }

        // GET: DonHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var donHang = await _context.DonHang
                .SingleOrDefaultAsync(m => m.DonHangID == id);
            if (donHang == null)
            {
                return NotFound();
            }
            var dsChiTiet = from ct in _context.ChiTietDonHang
                            where ct.DonHangID == id
                            select ct;
            foreach (var mh in dsChiTiet)
            {
                // var matHang = await _context.ChiDietDonHang.SingleOrDefaultAsync(m => m.DonHangID == mh.DonHangID);
                _context.ChiTietDonHang.Remove(mh);
            }
            _context.DonHang.Remove(donHang);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ThayDoiDaDuyet(int? idDH)
        {
            if (idDH == null)
            {
                return NotFound();
            }
            var donHang = await _context.DonHang
                .SingleOrDefaultAsync(m => m.DonHangID == idDH);
            donHang.DaDuyet = !donHang.DaDuyet;
            _context.Update(donHang);
            await _context.SaveChangesAsync();
            return Content("Thay đổi thành công");
        }
        public async Task<IActionResult> ThayDoiDaGiao(int? idDH)
        {
            if (idDH == null)
            {
                return NotFound();
            }
            
            var donHang = await _context.DonHang
                .SingleOrDefaultAsync(m => m.DonHangID == idDH);
            var khachHang = new EWareShop.Models.KhachHang();
            khachHang.HoTenKhachHang = donHang.TenKhachHang;
            khachHang.SoDienThoai = donHang.SoDienThoai;
            khachHang.DiaChi = donHang.DiaChi;
            khachHang.Email = donHang.Email;
            _context.KhachHang.Add(khachHang);
            await _context.SaveChangesAsync();

            var khachHangHienTai = await _context.KhachHang
                .SingleOrDefaultAsync(m => m.SoDienThoai == donHang.SoDienThoai);

            var danhSachChiTietDonHang = from ctdh in _context.ChiTietDonHang
                                         where ctdh.DonHangID == idDH
                                         select ctdh;

            var ngayBan = DateTime.Now;
            foreach(var ct in danhSachChiTietDonHang)
            {
                var sanPham = await _context.SanPham
                .SingleOrDefaultAsync(m => m.SanPhamID == ct.SanPhamID);
                var sanPhamBan = new SanPhamBan();
                sanPhamBan.KhachHangID = khachHangHienTai.KhachHangID;
                sanPhamBan.NgayBan = ngayBan;
                sanPhamBan.SoThangBaoHanh = sanPham.BaoHanh;

            }

            donHang.DaGiao = !donHang.DaGiao;
            _context.Update(donHang);
            await _context.SaveChangesAsync();
            return Content("Thay đổi thành công");
        }

        // POST: DonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donHang = await _context.DonHang.SingleOrDefaultAsync(m => m.DonHangID == id);
            _context.DonHang.Remove(donHang);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DonHangExists(int id)
        {
            return _context.DonHang.Any(e => e.DonHangID == id);
        }
    }
}
