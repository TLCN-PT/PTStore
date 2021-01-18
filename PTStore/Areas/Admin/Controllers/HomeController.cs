using Microsoft.AspNetCore.Mvc;
using PTStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace PTStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly PTStoreContext _context = new PTStoreContext();
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")) || HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Redirect("/Admin/Login");
            }
            ViewData["dh"] = _context.DonHangs.Count();
            ViewData["dt"] = _context.DienThoais.Count();
            ViewData["kh"] = _context.Accounts.Count() - 2;
            double x = 0;
            var dh = _context.DonHangs.Include(x => x.ChiTietDonHangs).Where(x => x.TrangTrai == "GiaoThanhCong");
            foreach (var item in dh)
            {
                foreach(var items in _context.ChiTietDonHangs.Where(x=>x.DonHangId==item.DonHangId))
                {
                    x += items.Gia.GetValueOrDefault() + items.SoLuong.GetValueOrDefault();
                }
            }
            ViewData["tt"] = string.Format("{0:0,0}",x);
            return View();
        }

        public IActionResult DangXuat()
        {
            if (IsCustomerLogged())
            {
                HttpContext.Session.Clear();
                return Redirect("/Admin/Login");
            }
            return Redirect("/Home/Error");
        }

        public bool IsCustomerLogged()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return false;
            }
            return true;
        }
    }
}
