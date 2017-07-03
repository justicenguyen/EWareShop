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

    public class LoaiSanPhamController : Controller
    {
        private readonly EWareShopContext _context;

        public LoaiSanPhamController(EWareShopContext context)
        {
            _context = context;
        }

        // GET: LoaiSanPham
        public async Task<IActionResult> Index()
        {
           
            return View(await _context.LoaiSanPham.ToListAsync());

        }

        // GET: LoaiSanPham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _context.LoaiSanPham
                .SingleOrDefaultAsync(m => m.LoaiSanPhamID == id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }

            return View(loaiSanPham);
        }

        // GET: LoaiSanPham/Create 
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TenLoaiSPCoDau,TenLoaiSPKhongDau")] LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiSanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(loaiSanPham);

        }

        // GET: LoaiSanPham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _context.LoaiSanPham.SingleOrDefaultAsync(m => m.LoaiSanPhamID == id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }
            return View(loaiSanPham);
        }

        // POST: LoaiSanPham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TenLoaiSPCoDau,TenLoaiSPKhongDau")] LoaiSanPham loaiSanPham)
        {
            if (id != loaiSanPham.LoaiSanPhamID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiSanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiSanPhamExists(loaiSanPham.LoaiSanPhamID))
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
            return View(loaiSanPham);
        }

        // GET: LoaiSanPham/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var loaiSanPham = await _context.LoaiSanPham.SingleOrDefaultAsync(m => m.LoaiSanPhamID == id);
            _context.LoaiSanPham.Remove(loaiSanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> XoaLSP(int? id)
        {
            var loaiSanPham = await _context.LoaiSanPham.SingleOrDefaultAsync(m => m.LoaiSanPhamID == id);
            _context.LoaiSanPham.Remove(loaiSanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create");
        }


        // POST: LoaiSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiSanPham = await _context.LoaiSanPham.SingleOrDefaultAsync(m => m.LoaiSanPhamID == id);
            _context.LoaiSanPham.Remove(loaiSanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create");
        }

        private bool LoaiSanPhamExists(int id)
        {
            return _context.LoaiSanPham.Any(e => e.LoaiSanPhamID == id);
        }
    }
}
