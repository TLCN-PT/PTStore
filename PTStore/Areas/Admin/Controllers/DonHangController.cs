using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PTStore.Models;

namespace PTStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonHangController : Controller
    {
        private readonly PTStoreContext _context;

        public DonHangController(PTStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/DonHang
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")) || HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Redirect("/Admin/Login");
            }
            var pTStoreContext = _context.DonHangs.Include(d => d.User).OrderByDescending(x=>x.NgayDatHang);
            return View(await pTStoreContext.ToListAsync());
        }

        // GET: Admin/DonHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHangs
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.DonHangId == id);
            if (donHang == null)
            {
                return NotFound();
            }

            if (donHang.HinhThucThanhToan == "TrucTiep")
                donHang.HinhThucThanhToan = "Trực tiếp";
            switch(donHang.TrangTrai)
            {
                case "DaGiaoHang":
                    donHang.TrangTrai = "Đã giao hàng";
                    break;
                case "DatHangThanhCong":
                    donHang.TrangTrai = "Đặt hàng thành công";
                    break;
                case "GiaoThanhCong":
                    donHang.TrangTrai = "Giao thành công";
                    break;
                case "BiHuy":
                    donHang.TrangTrai = "Bị huỷ";
                    break;
                default:
                    donHang.TrangTrai = "Lỗi";
                    break;
            }
            return View(donHang);
        }

        // GET: Admin/DonHang/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Admin/DonHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonHangId,MaDonHang,UserId,TenNguoiNhan,DiaChi,Email,SoDienThoai,NgayDatHang,NgayDuKienGiao,HinhThucThanhToan,TrangTrai")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", donHang.UserId);
            return View(donHang);
        }

        // GET: Admin/DonHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHangs.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", donHang.UserId);
            return View(donHang);
        }

        // POST: Admin/DonHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonHangId,MaDonHang,UserId,TenNguoiNhan,DiaChi,Email,SoDienThoai,NgayDatHang,NgayDuKienGiao,HinhThucThanhToan,TrangTrai")] DonHang donHang)
        {
            if (id != donHang.DonHangId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(donHang);
                    var qr = _context.DonHangs.Find(id);
                    qr.TrangTrai = donHang.TrangTrai;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonHangExists(donHang.DonHangId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", donHang.UserId);
            return View(donHang);
        }

        private bool DonHangExists(int id)
        {
            return _context.DonHangs.Any(e => e.DonHangId == id);
        }
    }
}
