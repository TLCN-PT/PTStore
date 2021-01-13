using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PTStore.Common.ViewModels;
using PTStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PTStore.Common.TienIch;

namespace PTStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly PTStoreContext _context = new PTStoreContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult LienHe()
        {
            return View();
        }

        // Check session if Customer logged or not
        public bool IsCustomerLogged()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return false;
            }
            return true;
        }

        // Layout Partial View
        [HttpGet]
        public IActionResult ThuongHieuPartial()
        {
            var qrGetThuongHieu = _context.ThuongHieus.ToList();
            return PartialView(qrGetThuongHieu);
        }

        public IActionResult HeaderMidPartial()
        {
            return PartialView();
        }

        public IActionResult HeaderBottomPartial()
        {
            var qrGetThuongHieu = _context.DienThoais.Include(x => x.ThuongHieu).Where(x => x.SoLuong > 0).Select(x => x.ThuongHieu).OrderBy(x => x.TenThuongHieu).Distinct();
            return PartialView(qrGetThuongHieu.ToList());
        }

        public IActionResult FooterPartial()
        {
            var qrGetThuongHieu = _context.DienThoais.Include(x => x.ThuongHieu).Where(x => x.SoLuong > 0).Select(x => x.ThuongHieu).OrderBy(x => x.TenThuongHieu).Distinct();
            return PartialView(qrGetThuongHieu.ToList());
        }

        public IActionResult DienThoaiDeXuatPartial()
        {
            var qrGetDT = _context.DienThoais.OrderByDescending(x => x.ChiTietDonHangs.Count).Take(9);
            return PartialView(qrGetDT.ToList());
        }

        public IActionResult DienThoaiGanDayPartial()
        {
            var qrGetDT = _context.DienThoais.OrderByDescending(x => x.ThongSoKyThuat.NgayRaMat).Take(4);
            return PartialView(qrGetDT.ToList());
        }

        // Error Message
        public IActionResult ErrorMessage()
        {
            return View();
        }

        public IActionResult DatLaiMatKhau()
        {
            return View();
        }

        public IActionResult TheoDoi(string email)
        {
            var qr = _context.Subcribers.Where(x => x.Email == email).FirstOrDefault();
            if(qr != null)
            {
                HttpContext.Session.SetString("TheoDoiMess", "DaDangKy");
            }
            else
            {
                HttpContext.Session.SetString("TheoDoiMess", "ThanhCong");
                _context.Subcribers.Add(new Subcriber
                {
                    Email = email,
                    TrangThai = "DangDangKy"
                });
                _context.SaveChanges();
                TIMail.GuiMailThongBaoDangKy(email);
            }    
            
            return Redirect("/Home");
        }
    }
}
