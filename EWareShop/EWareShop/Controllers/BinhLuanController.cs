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
    public class BinhLuanController : Controller
    {
        private readonly EWareShopContext _context;

        public BinhLuanController(EWareShopContext context)
        {
            _context = context;
        }

        // GET: BinhLuan
        public async Task<IActionResult> Index(string trang)
        {
            var duLieuBinhLuanIndex = new DuLieuBinhLuanIndex();
            var dsBinhLuan = from bl in _context.BinhLuan
                             select bl;

            //Phân trang
            int trangHienTai = 1;
            if (!String.IsNullOrEmpty(trang))
            {
                trangHienTai = int.Parse(trang);
            }
            int soSanPhamTrenTrang = 5;
            int soTrangSanPham = 0;
            if (dsBinhLuan.Count() % soSanPhamTrenTrang == 0)
            {
                soTrangSanPham = dsBinhLuan.Count() / soSanPhamTrenTrang;
            }
            else
            {
                soTrangSanPham = dsBinhLuan.Count() / soSanPhamTrenTrang + 1;
            }
            int viTri = (trangHienTai * soSanPhamTrenTrang - soSanPhamTrenTrang);
            dsBinhLuan = (from bl in dsBinhLuan
                                   orderby bl.BinhLuanID descending
                         select bl).Skip(viTri).Take(soSanPhamTrenTrang);

            duLieuBinhLuanIndex.DSBinhLuan = await dsBinhLuan.ToListAsync();
            duLieuBinhLuanIndex.TrangHienTai = trangHienTai;
            duLieuBinhLuanIndex.SoTrangSanPham = soTrangSanPham;
            return View(duLieuBinhLuanIndex);
        }

        // GET: BinhLuan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var binhLuan = await _context.BinhLuan
                .SingleOrDefaultAsync(m => m.BinhLuanID == id);
            if (binhLuan == null)
            {
                return NotFound();
            }

            return View(binhLuan);
        }

        // GET: BinhLuan/Create
        public IActionResult Create()
        {
            return RedirectToAction("TrangChu", "TrangChu");
            // return View();
        }

        // POST: BinhLuan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HoTen,Email,SoDienThoai,NoiDung")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(binhLuan);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(binhLuan);
        }

        // GET: BinhLuan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var binhLuan = await _context.BinhLuan.SingleOrDefaultAsync(m => m.BinhLuanID == id);
            if (binhLuan == null)
            {
                return NotFound();
            }
            return View(binhLuan);
        }

        // POST: BinhLuan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,HoTen,Email,SoDienThoai,NoiDung")] BinhLuan binhLuan)
        {
            if (id != binhLuan.BinhLuanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(binhLuan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BinhLuanExists(binhLuan.BinhLuanID))
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
            return View(binhLuan);
        }

        // GET: BinhLuan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var binhLuan = await _context.BinhLuan
                .SingleOrDefaultAsync(m => m.BinhLuanID == id);
            if (binhLuan == null)
            {
                return NotFound();
            }
            _context.BinhLuan.Remove(binhLuan);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: BinhLuan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var binhLuan = await _context.BinhLuan.SingleOrDefaultAsync(m => m.BinhLuanID == id);
            _context.BinhLuan.Remove(binhLuan);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BinhLuanExists(int id)
        {
            return _context.BinhLuan.Any(e => e.BinhLuanID == id);
        }

        //Ajax thay đổi kiểm duyệt
        public async Task<IActionResult> ThayDoiKiemDuyet(int? idbl)
        {
            if (idbl == null)
            {
                return NotFound();
            }

            var binhLuan = await _context.BinhLuan.SingleOrDefaultAsync(bl => bl.BinhLuanID == idbl);
            binhLuan.KiemDuyet = !binhLuan.KiemDuyet;
            _context.Update(binhLuan);
            await _context.SaveChangesAsync();
            return Content("Thành công");
        }
    }
}
