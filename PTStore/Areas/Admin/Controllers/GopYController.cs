using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PTStore.Models;

namespace PTStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GopYController : Controller
    {
        private readonly PTStoreContext _context;

        public GopYController(PTStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/GopY
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gopies.ToListAsync());
        }

        // GET: Admin/GopY/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gopY = await _context.Gopies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gopY == null)
            {
                return NotFound();
            }

            return View(gopY);
        }

        // GET: Admin/GopY/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/GopY/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,SoDienThoai,ChuDe,NoiDung")] GopY gopY)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gopY);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gopY);
        }

        // GET: Admin/GopY/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gopY = await _context.Gopies.FindAsync(id);
            if (gopY == null)
            {
                return NotFound();
            }
            return View(gopY);
        }

        // POST: Admin/GopY/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,SoDienThoai,ChuDe,NoiDung")] GopY gopY)
        {
            if (id != gopY.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gopY);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GopYExists(gopY.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gopY);
        }

        // GET: Admin/GopY/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gopY = await _context.Gopies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gopY == null)
            {
                return NotFound();
            }

            return View(gopY);
        }

        // POST: Admin/GopY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gopY = await _context.Gopies.FindAsync(id);
            _context.Gopies.Remove(gopY);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GopYExists(int id)
        {
            return _context.Gopies.Any(e => e.Id == id);
        }
    }
}
