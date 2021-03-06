﻿using Microsoft.AspNetCore.Mvc;
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
            if(!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                int id = int.Parse(HttpContext.Session.GetString("UserId"));
                var qr = _context.Users.Where(x => x.UserId == id).FirstOrDefault();
                ViewData["phone"] = qr.SoDienThoai;
                ViewData["email"] = qr.Email;
            }
            else
            {
                ViewData["phone"] = "";
                ViewData["email"] = "";
            }    
            return View();
        }

        [HttpPost]
        public IActionResult LienHe(string phone, string email, string subject, string message)
        {
            _context.Gopies.Add(new GopY
            {
                Email = email,
                SoDienThoai = phone,
                ChuDe = subject,
                NoiDung = message
            });
            _context.SaveChanges();
            HttpContext.Session.SetString("GopY", "1");
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
            var qrGetDT = _context.DienThoais.Include(x=>x.ThongSoKyThuat).OrderByDescending(x => x.ChiTietDonHangs.Count).Take(9);
            return PartialView(qrGetDT.ToList());
        }

        public IActionResult DienThoaiGanDayPartial()
        {
            var qrGetDT = _context.DienThoais.Include(x => x.ThongSoKyThuat).OrderByDescending(x => x.ThongSoKyThuat.NgayRaMat).Take(4);
            return PartialView(qrGetDT.ToList());
        }

        public IActionResult DienThoaiTrongTamGiaPartial()
        {
            int id = TIMail.idienthoai;
            var qrGetDT = _context.DienThoais.Include(x => x.ThongSoKyThuat).OrderBy(x => x.Gia).ToList();
            int index = qrGetDT.FindIndex(x => x.DienThoaiId == id);
            if (index == 0)
                return PartialView(qrGetDT.GetRange(1, 4));
            if (index == 1)
            {
                var s1 = qrGetDT.GetRange(0, 1);
                s1.AddRange(qrGetDT.GetRange(2, 3));
                s1.OrderBy(x => x.Gia);
                return PartialView(s1);
            }
            if (index == qrGetDT.Count - 2)
            {
                var s1 = qrGetDT.GetRange(qrGetDT.Count - 1, 1);
                s1.AddRange(qrGetDT.GetRange(qrGetDT.Count - 5, 3));
                s1.OrderBy(x => x.Gia);
                return PartialView(s1);
            }
            if (index == qrGetDT.Count-1)
                return PartialView(qrGetDT.GetRange(qrGetDT.Count - 5, 4));
            var s = qrGetDT.GetRange(index - 2, 2);
            s.AddRange(qrGetDT.GetRange(index +1, 2));
            s.OrderBy(x => x.Gia);
            return PartialView(s);
        }
        // Error Message
        public IActionResult ErrorMessage()
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
