using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PTStore.Models;
using PTStore.Common.ViewModels.Admin;
using Microsoft.AspNetCore.Http;

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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")) || HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Redirect("/Admin/Login");
            }
            var pTStoreContext = _context.DienThoais.Include(d => d.ThongSoKyThuat).Include(d => d.ThuongHieu).ToList();
            List<DienThoaiIndexViewModel> lstDt = new List<DienThoaiIndexViewModel>();
            foreach (var item in pTStoreContext)
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

            return View(lstDt);
        }

        [HttpPost]
        public IActionResult Index(string search)
        {
            var pTStoreContext = _context.DienThoais.Include(d => d.ThongSoKyThuat).Include(d => d.ThuongHieu).ToList();
            List<DienThoaiIndexViewModel> lstDt = new List<DienThoaiIndexViewModel>();
            foreach (var item in pTStoreContext)
            {
                if (item.Name.ToUpper().Contains(search.ToUpper()))
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
        public async Task<IActionResult> Create([Bind("DienThoaiId,Name,TinhTrang,HinhAnh,Gia,GiaGoc,SoLuong,ThuongHieuId,ThongSoKyThuat")] DienThoai dienThoai)
        {
            if (ModelState.IsValid)
            {
                _context.DienThoais.Add(
                    new DienThoai
                    {
                        Name = dienThoai.Name,
                        TinhTrang = dienThoai.TinhTrang,
                        HinhAnh = dienThoai.HinhAnh,
                        Gia = dienThoai.Gia,
                        GiaGoc = dienThoai.GiaGoc,
                        SoLuong = dienThoai.SoLuong,
                        ThuongHieuId = dienThoai.ThuongHieuId,
                        ThongSoKyThuat = new ThongSoKyThuat
                        {
                            ManHinh = dienThoai.ThongSoKyThuat.ManHinh,
                            HeDieuHanh = dienThoai.ThongSoKyThuat.HeDieuHanh,
                            CameraSau = dienThoai.ThongSoKyThuat.CameraSau,
                            CameraTruoc = dienThoai.ThongSoKyThuat.CameraTruoc,
                            Cpu = dienThoai.ThongSoKyThuat.Cpu,
                            BoNhoTrong = dienThoai.ThongSoKyThuat.BoNhoTrong,
                            Ram = dienThoai.ThongSoKyThuat.Ram,
                            TheSim = dienThoai.ThongSoKyThuat.TheSim,
                            DungLuongPin = dienThoai.ThongSoKyThuat.DungLuongPin,
                            NgayRaMat = dienThoai.ThongSoKyThuat.NgayRaMat
                        }
                    });
                _context.SaveChanges();
                var qr2 = _context.ThongSoKyThuats.Where(x => x.ManHinh == dienThoai.ThongSoKyThuat.ManHinh
                && x.HeDieuHanh == dienThoai.ThongSoKyThuat.HeDieuHanh &&
                            x.CameraSau == dienThoai.ThongSoKyThuat.CameraSau &&
                            x.CameraTruoc == dienThoai.ThongSoKyThuat.CameraTruoc &&
                            x.Cpu == dienThoai.ThongSoKyThuat.Cpu &&
                            x.BoNhoTrong == dienThoai.ThongSoKyThuat.BoNhoTrong &&
                            x.Ram == dienThoai.ThongSoKyThuat.Ram &&
                            x.TheSim == dienThoai.ThongSoKyThuat.TheSim &&
                            x.DungLuongPin == dienThoai.ThongSoKyThuat.DungLuongPin).FirstOrDefault();
                qr2.DienThoaiId = _context.DienThoais.Where(x => x.Name == dienThoai.Name).FirstOrDefault().DienThoaiId;
                _context.SaveChanges();
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

            var dienThoai = await _context.DienThoais.Include(x => x.ThongSoKyThuat).Where(x => x.DienThoaiId == id).FirstOrDefaultAsync();
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
        public async Task<IActionResult> Edit(int id, [Bind("DienThoaiId,Name,TinhTrang,HinhAnh,Gia,GiaGoc,SoLuong,ThuongHieuId,ThongSoKyThuatId,ThongSoKyThuat")] DienThoai dienThoai)
        {
            if (id != dienThoai.DienThoaiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var qr = _context.DienThoais.Find(id);
                    qr.Name = dienThoai.Name;
                    qr.HinhAnh = dienThoai.HinhAnh;
                    qr.Gia = dienThoai.Gia;
                    qr.GiaGoc = dienThoai.GiaGoc;
                    qr.TinhTrang = dienThoai.TinhTrang;
                    qr.SoLuong = dienThoai.SoLuong;
                    qr.ThuongHieuId = dienThoai.ThuongHieuId;
                    var qr2 = _context.ThongSoKyThuats.Find(dienThoai.ThongSoKyThuatId);
                    qr2.ManHinh = dienThoai.ThongSoKyThuat.ManHinh;
                    qr2.HeDieuHanh = dienThoai.ThongSoKyThuat.HeDieuHanh;
                    qr2.CameraSau = dienThoai.ThongSoKyThuat.CameraSau;
                    qr2.CameraTruoc = dienThoai.ThongSoKyThuat.CameraTruoc;
                    qr2.Cpu = dienThoai.ThongSoKyThuat.Cpu;
                    qr2.BoNhoTrong = dienThoai.ThongSoKyThuat.BoNhoTrong;
                    qr2.Ram = dienThoai.ThongSoKyThuat.Ram;
                    qr2.TheSim = dienThoai.ThongSoKyThuat.TheSim;
                    qr2.DungLuongPin = dienThoai.ThongSoKyThuat.DungLuongPin;
                    qr2.NgayRaMat = dienThoai.ThongSoKyThuat.NgayRaMat;
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

        private bool DienThoaiExists(int id)
        {
            return _context.DienThoais.Any(e => e.DienThoaiId == id);
        }
    }
}
