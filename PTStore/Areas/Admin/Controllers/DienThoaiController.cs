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
    public class DienThoaiController : Controller
    {
        private readonly PTStoreContext _context;

        public DienThoaiController(PTStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/DienThoai
        public async Task<IActionResult> Index()
        {
            var pTStoreContext = _context.DienThoais.Include(d => d.ThongSoKyThuat).Include(d => d.ThuongHieu);
            return View(await pTStoreContext.ToListAsync());
        }

        // GET: Admin/DienThoai/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dienThoai = await _context.DienThoais
                .Include(d => d.ThongSoKyThuat)
                .Include(d => d.ThuongHieu)
                .FirstOrDefaultAsync(m => m.DienThoaiId == id);
            if (dienThoai == null)
            {
                return NotFound();
            }

            return View(dienThoai);
        }

        // GET: Admin/DienThoai/Create
        public IActionResult Create()
        {
            ViewData["ThongSoKyThuatId"] = new SelectList(_context.ThongSoKyThuats, "ThongSoKyThuatId", "ThongSoKyThuatId");
            ViewData["ThuongHieuId"] = new SelectList(_context.ThuongHieus, "ThuongHieuId", "TenThuongHieu");
            return View();
        }

        // POST: Admin/DienThoai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DienThoaiId,Name,TinhTrang,HinhAnh,Gia,GiaGoc,SoLuong,ThuongHieuId,ThongSoKyThuatId")] DienThoai dienThoai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dienThoai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ThongSoKyThuatId"] = new SelectList(_context.ThongSoKyThuats, "ThongSoKyThuatId", "ThongSoKyThuatId", dienThoai.ThongSoKyThuatId);
            ViewData["ThuongHieuId"] = new SelectList(_context.ThuongHieus, "ThuongHieuId", "TenThuongHieu", dienThoai.ThuongHieuId);
            return View(dienThoai);
        }

        // GET: Admin/DienThoai/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dienThoai = await _context.DienThoais.FindAsync(id);
            if (dienThoai == null)
            {
                return NotFound();
            }
            ViewData["ThongSoKyThuatId"] = new SelectList(_context.ThongSoKyThuats, "ThongSoKyThuatId", "ThongSoKyThuatId", dienThoai.ThongSoKyThuatId);
            ViewData["ThuongHieuId"] = new SelectList(_context.ThuongHieus, "ThuongHieuId", "TenThuongHieu", dienThoai.ThuongHieuId);
            return View(dienThoai);
        }

        // POST: Admin/DienThoai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DienThoaiId,Name,TinhTrang,HinhAnh,Gia,GiaGoc,SoLuong,ThuongHieuId,ThongSoKyThuatId")] DienThoai dienThoai)
        {
            if (id != dienThoai.DienThoaiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dienThoai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DienThoaiExists(dienThoai.DienThoaiId))
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
            ViewData["ThongSoKyThuatId"] = new SelectList(_context.ThongSoKyThuats, "ThongSoKyThuatId", "ThongSoKyThuatId", dienThoai.ThongSoKyThuatId);
            ViewData["ThuongHieuId"] = new SelectList(_context.ThuongHieus, "ThuongHieuId", "TenThuongHieu", dienThoai.ThuongHieuId);
            return View(dienThoai);
        }

        // GET: Admin/DienThoai/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dienThoai = await _context.DienThoais
                .Include(d => d.ThongSoKyThuat)
                .Include(d => d.ThuongHieu)
                .FirstOrDefaultAsync(m => m.DienThoaiId == id);
            if (dienThoai == null)
            {
                return NotFound();
            }

            return View(dienThoai);
        }

        // POST: Admin/DienThoai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dienThoai = await _context.DienThoais.FindAsync(id);
            _context.DienThoais.Remove(dienThoai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DienThoaiExists(int id)
        {
            return _context.DienThoais.Any(e => e.DienThoaiId == id);
        }
    }
}
