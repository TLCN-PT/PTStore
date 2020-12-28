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
    public class ChiTietDonHangsController : Controller
    {
        private readonly PTStoreContext _context;

        public ChiTietDonHangsController(PTStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/ChiTietDonHangs
        public async Task<IActionResult> Index()
        {
            var pTStoreContext = _context.ChiTietDonHangs.Include(c => c.DienThoai).Include(c => c.DonHang);
            return View(await pTStoreContext.ToListAsync());
        }

        // GET: Admin/ChiTietDonHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietDonHang = await _context.ChiTietDonHangs
                .Include(c => c.DienThoai)
                .Include(c => c.DonHang)
                .FirstOrDefaultAsync(m => m.DonHangId == id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }

            return View(chiTietDonHang);
        }

        // GET: Admin/ChiTietDonHangs/Create
        public IActionResult Create()
        {
            ViewData["DienThoaiId"] = new SelectList(_context.DienThoais, "DienThoaiId", "DienThoaiId");
            ViewData["DonHangId"] = new SelectList(_context.DonHangs, "DonHangId", "DonHangId");
            return View();
        }

        // POST: Admin/ChiTietDonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonHangId,DienThoaiId,SoLuong,Gia")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietDonHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DienThoaiId"] = new SelectList(_context.DienThoais, "DienThoaiId", "DienThoaiId", chiTietDonHang.DienThoaiId);
            ViewData["DonHangId"] = new SelectList(_context.DonHangs, "DonHangId", "DonHangId", chiTietDonHang.DonHangId);
            return View(chiTietDonHang);
        }

        // GET: Admin/ChiTietDonHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietDonHang = await _context.ChiTietDonHangs.FindAsync(id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }
            ViewData["DienThoaiId"] = new SelectList(_context.DienThoais, "DienThoaiId", "DienThoaiId", chiTietDonHang.DienThoaiId);
            ViewData["DonHangId"] = new SelectList(_context.DonHangs, "DonHangId", "DonHangId", chiTietDonHang.DonHangId);
            return View(chiTietDonHang);
        }

        // POST: Admin/ChiTietDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonHangId,DienThoaiId,SoLuong,Gia")] ChiTietDonHang chiTietDonHang)
        {
            if (id != chiTietDonHang.DonHangId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietDonHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietDonHangExists(chiTietDonHang.DonHangId))
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
            ViewData["DienThoaiId"] = new SelectList(_context.DienThoais, "DienThoaiId", "DienThoaiId", chiTietDonHang.DienThoaiId);
            ViewData["DonHangId"] = new SelectList(_context.DonHangs, "DonHangId", "DonHangId", chiTietDonHang.DonHangId);
            return View(chiTietDonHang);
        }

        // GET: Admin/ChiTietDonHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietDonHang = await _context.ChiTietDonHangs
                .Include(c => c.DienThoai)
                .Include(c => c.DonHang)
                .FirstOrDefaultAsync(m => m.DonHangId == id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }

            return View(chiTietDonHang);
        }

        // POST: Admin/ChiTietDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietDonHang = await _context.ChiTietDonHangs.FindAsync(id);
            _context.ChiTietDonHangs.Remove(chiTietDonHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietDonHangExists(int id)
        {
            return _context.ChiTietDonHangs.Any(e => e.DonHangId == id);
        }
    }
}
