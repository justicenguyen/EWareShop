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
    public class NhaSanXuatController : Controller
    {
        private readonly EWareShopContext _context;

        public NhaSanXuatController(EWareShopContext context)
        {
            _context = context;
        }

        // GET: NhaSanXuats
        public async Task<IActionResult> Index()
        {
            return View(await _context.NhaSanXuat.ToListAsync());
        }

        // GET: NhaSanXuats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaSanXuat = await _context.NhaSanXuat
                .SingleOrDefaultAsync(m => m.NhaSanXuatID == id);
            if (nhaSanXuat == null)
            {
                return NotFound();
            }

            return View(nhaSanXuat);
        }

        // GET: NhaSanXuats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NhaSanXuats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TenNSXCoDau,TenNSXKhongDau")] NhaSanXuat nhaSanXuat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhaSanXuat);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nhaSanXuat);
        }

        // GET: NhaSanXuats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaSanXuat = await _context.NhaSanXuat.SingleOrDefaultAsync(m => m.NhaSanXuatID == id);
            if (nhaSanXuat == null)
            {
                return NotFound();
            }
            return View(nhaSanXuat);
        }

        // POST: NhaSanXuats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TenNSXCoDau,TenNSXKhongDau")] NhaSanXuat nhaSanXuat)
        {
            if (id != nhaSanXuat.NhaSanXuatID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhaSanXuat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhaSanXuatExists(nhaSanXuat.NhaSanXuatID))
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
            return View(nhaSanXuat);
        }

        // GET: NhaSanXuats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var nhaSanXuat = await _context.NhaSanXuat
                .SingleOrDefaultAsync(m => m.NhaSanXuatID == id);
            if (nhaSanXuat == null)
            {
                return NotFound();
            }
            _context.NhaSanXuat.Remove(nhaSanXuat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: NhaSanXuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhaSanXuat = await _context.NhaSanXuat.SingleOrDefaultAsync(m => m.NhaSanXuatID == id);
            _context.NhaSanXuat.Remove(nhaSanXuat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool NhaSanXuatExists(int id)
        {
            return _context.NhaSanXuat.Any(e => e.NhaSanXuatID == id);
        }
    }
}
