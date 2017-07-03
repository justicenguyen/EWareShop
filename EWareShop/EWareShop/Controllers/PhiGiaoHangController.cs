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
    public class PhiGiaoHangController : Controller
    {
        private readonly EWareShopContext _context;

        public PhiGiaoHangController(EWareShopContext context)
        {
            _context = context;
        }

        // GET: PhiGiaoHang
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhiGiaoHang.ToListAsync());
        }

        // GET: PhiGiaoHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phiGiaoHang = await _context.PhiGiaoHang
                .SingleOrDefaultAsync(m => m.PhiGiaoHangID == id);
            if (phiGiaoHang == null)
            {
                return NotFound();
            }

            return View(phiGiaoHang);
        }

        // GET: PhiGiaoHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhiGiaoHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhiGiaoHangID,DiaDiem,TienPhi")] PhiGiaoHang phiGiaoHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phiGiaoHang);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(phiGiaoHang);
        }

        // GET: PhiGiaoHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phiGiaoHang = await _context.PhiGiaoHang.SingleOrDefaultAsync(m => m.PhiGiaoHangID == id);
            if (phiGiaoHang == null)
            {
                return NotFound();
            }
            return View(phiGiaoHang);
        }

        // POST: PhiGiaoHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhiGiaoHangID,DiaDiem,TienPhi")] PhiGiaoHang phiGiaoHang)
        {
            if (id != phiGiaoHang.PhiGiaoHangID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phiGiaoHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhiGiaoHangExists(phiGiaoHang.PhiGiaoHangID))
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
            return View(phiGiaoHang);
        }

        // GET: PhiGiaoHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phiGiaoHang = await _context.PhiGiaoHang
                .SingleOrDefaultAsync(m => m.PhiGiaoHangID == id);
            if (phiGiaoHang == null)
            {
                return NotFound();
            }

            _context.PhiGiaoHang.Remove(phiGiaoHang);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: PhiGiaoHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phiGiaoHang = await _context.PhiGiaoHang.SingleOrDefaultAsync(m => m.PhiGiaoHangID == id);
            _context.PhiGiaoHang.Remove(phiGiaoHang);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PhiGiaoHangExists(int id)
        {
            return _context.PhiGiaoHang.Any(e => e.PhiGiaoHangID == id);
        }
    }
}
