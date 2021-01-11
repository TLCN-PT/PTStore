using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PTStore.Models;
using PTStore.Common.ViewModels.Admin;

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
        public IActionResult Index()
        {
            var pTStoreContext = _context.DienThoais.Include(d => d.ThongSoKyThuat).Include(d => d.ThuongHieu).ToList();
            List<DienThoaiIndexViewModel> lstDt = new List<DienThoaiIndexViewModel>();
            foreach(var item in pTStoreContext)
            {
                DienThoaiIndexViewModel dt = new DienThoaiIndexViewModel();
                dt.dienThoai = item;
                if(dt.dienThoai.TinhTrang == "DangKinhDoanh")
                {
                    dt.dienThoai.TinhTrang = "Đang kinh doanh";
                }
                else
                {
                    dt.dienThoai.TinhTrang = "Ngừng kinh doanh";
                }    
                dt.TenThuongHieu = _context.ThuongHieus.Where(x => x.ThuongHieuId == item.ThuongHieuId).FirstOrDefault().TenThuongHieu;
                lstDt.Add(dt);
            }
            
            return View(lstDt);
        }

        [HttpPost]
        public IActionResult Index(string search)
        {
            var pTStoreContext = _context.DienThoais.Include(d => d.ThongSoKyThuat).Include(d => d.ThuongHieu).ToList();
            List<DienThoaiIndexViewModel> lstDt = new List<DienThoaiIndexViewModel>();
            foreach (var item in pTStoreContext)
            {
                if(item.Name.Contains(search))
                {
                    DienThoaiIndexViewModel dt = new DienThoaiIndexViewModel();
                    dt.dienThoai = item;
                    if (dt.dienThoai.TinhTrang == "DangKinhDoanh")
                    {
                        dt.dienThoai.TinhTrang = "Đang kinh doanh";
                    }
                    else
                    {
                        dt.dienThoai.TinhTrang = "Ngừng kinh doanh";
                    }
                    dt.TenThuongHieu = _context.ThuongHieus.Where(x => x.ThuongHieuId == item.ThuongHieuId).FirstOrDefault().TenThuongHieu;
                    lstDt.Add(dt);
                }
                
            }

            return View(lstDt);
        }

        // GET: Admin/DienThoai/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dienThoai = _context.DienThoais
                .Include(d => d.ThongSoKyThuat)
                .Include(d => d.ThuongHieu)
                .FirstOrDefault(m => m.DienThoaiId == id);
            if (dienThoai == null)
            {
                return NotFound();
            }
            if (dienThoai.TinhTrang == "DangKinhDoanh")
            {
                dienThoai.TinhTrang = "Đang kinh doanh";
            }
            else
                dienThoai.TinhTrang = "Ngừng kinh doanh";
            //DienThoaiDetailViewModel dt = new DienThoaiDetailViewModel();
            //dt.dienThoai = dienThoai;
            //dt.thongSoKyThuat = _context.ThongSoKyThuats.Where(x => x.DienThoaiId == dt.dienThoai.DienThoaiId).FirstOrDefault();
            //dt.TenThuongHieu = _context.ThuongHieus.Where(x => x.ThuongHieuId == dt.dienThoai.ThuongHieuId).FirstOrDefault().TenThuongHieu;
            //dienThoai.ThuongHieu.
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
