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
    public class SubcriberController : Controller
    {
        private readonly PTStoreContext _context;

        public SubcriberController(PTStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/Subcriber
        public async Task<IActionResult> Index()
        {
            return View(await _context.Subcribers.ToListAsync());
        }

        // GET: Admin/Subcriber/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcriber = await _context.Subcribers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subcriber == null)
            {
                return NotFound();
            }
            if(subcriber.TrangThai == "DangDangKy")
            {
                subcriber.TrangThai = "Đang đăng ký";
            }
            else
            {
                subcriber.TrangThai = "Ngừng đăng ký";
            }
            return View(subcriber);
        }

        // GET: Admin/Subcriber/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Subcriber/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,TrangThai")] Subcriber subcriber)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subcriber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subcriber);
        }

        // GET: Admin/Subcriber/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcriber = await _context.Subcribers.FindAsync(id);
            if (subcriber == null)
            {
                return NotFound();
            }
            return View(subcriber);
        }

        // POST: Admin/Subcriber/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,TrangThai")] Subcriber subcriber)
        {
            if (id != subcriber.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subcriber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubcriberExists(subcriber.Id))
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
            return View(subcriber);
        }

        private bool SubcriberExists(int id)
        {
            return _context.Subcribers.Any(e => e.Id == id);
        }
    }
}
