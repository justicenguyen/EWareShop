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
    public class LienHeController : Controller
    {
        private readonly EWareShopContext _context;

        public LienHeController(EWareShopContext context)
        {
            _context = context;    
        }

        // GET: LienHe
        public async Task<IActionResult> Index()
        {
            return View(await _context.LienHe.ToListAsync());
        }

        // GET: LienHe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lienHe = await _context.LienHe
                .SingleOrDefaultAsync(m => m.LienHeID == id);
            if (lienHe == null)
            {
                return NotFound();
            }

            return View(lienHe);
        }

        // GET: LienHe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LienHe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LienHeID,HoTen,Email,SoDienThoai,VanDe,NgayGui")] LienHe lienHe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lienHe);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lienHe);
        }

        // GET: LienHe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lienHe = await _context.LienHe.SingleOrDefaultAsync(m => m.LienHeID == id);
            if (lienHe == null)
            {
                return NotFound();
            }
            return View(lienHe);
        }

        // POST: LienHe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LienHeID,HoTen,Email,SoDienThoai,VanDe,NgayGui")] LienHe lienHe)
        {
            if (id != lienHe.LienHeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lienHe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LienHeExists(lienHe.LienHeID))
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
            return View(lienHe);
        }

        // GET: LienHe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lienHe = await _context.LienHe
                .SingleOrDefaultAsync(m => m.LienHeID == id);
            if (lienHe == null)
            {
                return NotFound();
            }
            _context.LienHe.Remove(lienHe);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: LienHe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lienHe = await _context.LienHe.SingleOrDefaultAsync(m => m.LienHeID == id);
            _context.LienHe.Remove(lienHe);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LienHeExists(int id)
        {
            return _context.LienHe.Any(e => e.LienHeID == id);
        }
        public IActionResult LienHe()
        {
            ViewBag.daGui = TempData["coLienHe"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LienHe([Bind("LienHeID,HoTen,Email,SoDienThoai,VanDe")] LienHe lienHe)
        {
            if (ModelState.IsValid)
            {
                var thoiGianHienTai = DateTime.Now;
                lienHe.NgayGui = thoiGianHienTai;
                _context.Add(lienHe);
                await _context.SaveChangesAsync();
                TempData["coLienHe"] = "coLienHe";
                return RedirectToAction("LienHe");
                
            }
            return View(lienHe);
        }

        public async Task<IActionResult> ThayDoiDaTraLoi(int idlh)
        {
            var lienHe = await _context.LienHe.SingleOrDefaultAsync(lh => lh.LienHeID == idlh);
            lienHe.DaTraLoi = !lienHe.DaTraLoi;
            await _context.SaveChangesAsync();
            return Content("Thay đổi thành công");
        }
    }
}
