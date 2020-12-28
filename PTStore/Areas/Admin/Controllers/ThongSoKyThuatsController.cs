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
    public class ThongSoKyThuatsController : Controller
    {
        private readonly PTStoreContext _context;

        public ThongSoKyThuatsController(PTStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/ThongSoKyThuats
        public async Task<IActionResult> Index()
        {
            var pTStoreContext = _context.ThongSoKyThuats.Include(t => t.DienThoai);
            return View(await pTStoreContext.ToListAsync());
        }

        // GET: Admin/ThongSoKyThuats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongSoKyThuat = await _context.ThongSoKyThuats
                .Include(t => t.DienThoai)
                .FirstOrDefaultAsync(m => m.ThongSoKyThuatId == id);
            if (thongSoKyThuat == null)
            {
                return NotFound();
            }

            return View(thongSoKyThuat);
        }

        // GET: Admin/ThongSoKyThuats/Create
        public IActionResult Create()
        {
            ViewData["DienThoaiId"] = new SelectList(_context.DienThoais, "DienThoaiId", "DienThoaiId");
            return View();
        }

        // POST: Admin/ThongSoKyThuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThongSoKyThuatId,ManHinh,HeDieuHanh,CameraSau,CameraTruoc,Cpu,Ram,BoNhoTrong,TheSim,DungLuongPin,NgayRaMat,DienThoaiId")] ThongSoKyThuat thongSoKyThuat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thongSoKyThuat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DienThoaiId"] = new SelectList(_context.DienThoais, "DienThoaiId", "DienThoaiId", thongSoKyThuat.DienThoaiId);
            return View(thongSoKyThuat);
        }

        // GET: Admin/ThongSoKyThuats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongSoKyThuat = await _context.ThongSoKyThuats.FindAsync(id);
            if (thongSoKyThuat == null)
            {
                return NotFound();
            }
            ViewData["DienThoaiId"] = new SelectList(_context.DienThoais, "DienThoaiId", "DienThoaiId", thongSoKyThuat.DienThoaiId);
            return View(thongSoKyThuat);
        }

        // POST: Admin/ThongSoKyThuats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThongSoKyThuatId,ManHinh,HeDieuHanh,CameraSau,CameraTruoc,Cpu,Ram,BoNhoTrong,TheSim,DungLuongPin,NgayRaMat,DienThoaiId")] ThongSoKyThuat thongSoKyThuat)
        {
            if (id != thongSoKyThuat.ThongSoKyThuatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thongSoKyThuat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongSoKyThuatExists(thongSoKyThuat.ThongSoKyThuatId))
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
            ViewData["DienThoaiId"] = new SelectList(_context.DienThoais, "DienThoaiId", "DienThoaiId", thongSoKyThuat.DienThoaiId);
            return View(thongSoKyThuat);
        }

        // GET: Admin/ThongSoKyThuats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thongSoKyThuat = await _context.ThongSoKyThuats
                .Include(t => t.DienThoai)
                .FirstOrDefaultAsync(m => m.ThongSoKyThuatId == id);
            if (thongSoKyThuat == null)
            {
                return NotFound();
            }

            return View(thongSoKyThuat);
        }

        // POST: Admin/ThongSoKyThuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thongSoKyThuat = await _context.ThongSoKyThuats.FindAsync(id);
            _context.ThongSoKyThuats.Remove(thongSoKyThuat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThongSoKyThuatExists(int id)
        {
            return _context.ThongSoKyThuats.Any(e => e.ThongSoKyThuatId == id);
        }
    }
}
