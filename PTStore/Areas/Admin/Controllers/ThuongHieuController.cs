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
    public class ThuongHieuController : Controller
    {
        private readonly PTStoreContext _context;

        public ThuongHieuController(PTStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/ThuongHieu
        public async Task<IActionResult> Index()
        {
            return View(await _context.ThuongHieus.ToListAsync());
        }

        // GET: Admin/ThuongHieu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thuongHieu = await _context.ThuongHieus
                .FirstOrDefaultAsync(m => m.ThuongHieuId == id);
            if (thuongHieu == null)
            {
                return NotFound();
            }

            return View(thuongHieu);
        }

        // GET: Admin/ThuongHieu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ThuongHieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThuongHieuId,TenThuongHieu,HinhAnhThuongHieu")] ThuongHieu thuongHieu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thuongHieu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thuongHieu);
        }

        // GET: Admin/ThuongHieu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thuongHieu = await _context.ThuongHieus.FindAsync(id);
            if (thuongHieu == null)
            {
                return NotFound();
            }
            return View(thuongHieu);
        }

        // POST: Admin/ThuongHieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThuongHieuId,TenThuongHieu,HinhAnhThuongHieu")] ThuongHieu thuongHieu)
        {
            if (id != thuongHieu.ThuongHieuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thuongHieu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThuongHieuExists(thuongHieu.ThuongHieuId))
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
            return View(thuongHieu);
        }

        private bool ThuongHieuExists(int id)
        {
            return _context.ThuongHieus.Any(e => e.ThuongHieuId == id);
        }
    }
}
