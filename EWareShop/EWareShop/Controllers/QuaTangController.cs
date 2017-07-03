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
    public class QuaTangController : Controller
    {
        private readonly EWareShopContext _context;

        public QuaTangController(EWareShopContext context)
        {
            _context = context;    
        }

        // GET: QuaTang
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuaTang.ToListAsync());
        }

        // GET: QuaTang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quaTang = await _context.QuaTang
                .SingleOrDefaultAsync(m => m.QuaTangID == id);
            if (quaTang == null)
            {
                return NotFound();
            }

            return View(quaTang);
        }

        // GET: QuaTang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuaTang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuaTangID,TenQuaTang")] QuaTang quaTang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quaTang);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(quaTang);
        }

        // GET: QuaTang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quaTang = await _context.QuaTang.SingleOrDefaultAsync(m => m.QuaTangID == id);
            if (quaTang == null)
            {
                return NotFound();
            }
            return View(quaTang);
        }

        // POST: QuaTang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuaTangID,TenQuaTang")] QuaTang quaTang)
        {
            if (id != quaTang.QuaTangID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quaTang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuaTangExists(quaTang.QuaTangID))
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
            return View(quaTang);
        }

        // GET: QuaTang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quaTang = await _context.QuaTang
                .SingleOrDefaultAsync(m => m.QuaTangID == id);
            if (quaTang == null)
            {
                return NotFound();
            }

            return View(quaTang);
        }

        // POST: QuaTang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quaTang = await _context.QuaTang.SingleOrDefaultAsync(m => m.QuaTangID == id);
            _context.QuaTang.Remove(quaTang);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool QuaTangExists(int id)
        {
            return _context.QuaTang.Any(e => e.QuaTangID == id);
        }
    }
}
