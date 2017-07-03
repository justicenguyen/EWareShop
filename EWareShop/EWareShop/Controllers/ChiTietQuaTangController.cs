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
    public class ChiTietQuaTangController : Controller
    {
        private readonly EWareShopContext _context;

        public ChiTietQuaTangController(EWareShopContext context)
        {
            _context = context;    
        }

        // GET: ChiTietQuaTang
        public async Task<IActionResult> Index()
        {
            var eWareShopContext = _context.ChiTietQuaTang.Include(c => c.QuaTang).Include(c => c.SanPham);
            return View(await eWareShopContext.ToListAsync());
        }

        // GET: ChiTietQuaTang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietQuaTang = await _context.ChiTietQuaTang
                .Include(c => c.QuaTang)
                .Include(c => c.SanPham)
                .SingleOrDefaultAsync(m => m.SanPhamID == id);
            if (chiTietQuaTang == null)
            {
                return NotFound();
            }

            return View(chiTietQuaTang);
        }

        // GET: ChiTietQuaTang/Create
        public IActionResult Create()
        {
            ViewData["QuaTangID"] = new SelectList(_context.Set<QuaTang>(), "QuaTangID", "TenQuaTang");
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "HinhAnh");
            return View();
        }

        // POST: ChiTietQuaTang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SanPhamID,QuaTangID")] ChiTietQuaTang chiTietQuaTang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietQuaTang);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["QuaTangID"] = new SelectList(_context.Set<QuaTang>(), "QuaTangID", "TenQuaTang", chiTietQuaTang.QuaTangID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "HinhAnh", chiTietQuaTang.SanPhamID);
            return View(chiTietQuaTang);
        }

        // GET: ChiTietQuaTang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietQuaTang = await _context.ChiTietQuaTang.SingleOrDefaultAsync(m => m.SanPhamID == id);
            if (chiTietQuaTang == null)
            {
                return NotFound();
            }
            ViewData["QuaTangID"] = new SelectList(_context.Set<QuaTang>(), "QuaTangID", "TenQuaTang", chiTietQuaTang.QuaTangID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "HinhAnh", chiTietQuaTang.SanPhamID);
            return View(chiTietQuaTang);
        }

        // POST: ChiTietQuaTang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SanPhamID,QuaTangID")] ChiTietQuaTang chiTietQuaTang)
        {
            if (id != chiTietQuaTang.SanPhamID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietQuaTang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietQuaTangExists(chiTietQuaTang.SanPhamID))
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
            ViewData["QuaTangID"] = new SelectList(_context.Set<QuaTang>(), "QuaTangID", "TenQuaTang", chiTietQuaTang.QuaTangID);
            ViewData["SanPhamID"] = new SelectList(_context.SanPham, "SanPhamID", "HinhAnh", chiTietQuaTang.SanPhamID);
            return View(chiTietQuaTang);
        }

        // GET: ChiTietQuaTang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietQuaTang = await _context.ChiTietQuaTang
                .Include(c => c.QuaTang)
                .Include(c => c.SanPham)
                .SingleOrDefaultAsync(m => m.SanPhamID == id);
            if (chiTietQuaTang == null)
            {
                return NotFound();
            }

            return View(chiTietQuaTang);
        }

        // POST: ChiTietQuaTang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietQuaTang = await _context.ChiTietQuaTang.SingleOrDefaultAsync(m => m.SanPhamID == id);
            _context.ChiTietQuaTang.Remove(chiTietQuaTang);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ChiTietQuaTangExists(int id)
        {
            return _context.ChiTietQuaTang.Any(e => e.SanPhamID == id);
        }
    }
}
