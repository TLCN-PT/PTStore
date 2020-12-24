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
            ViewData.Model = _context.ThuongHieus.ToList();
            //ViewData["Name"] = s.NgayDatHang?.ToString("dd/M/yyyy");
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

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Productbytype()
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Login()
        {
            ViewData["ErrorModel"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel acc)
        {
            if (ModelState.IsValid)
            {
                var query = _context.Accounts.Where(s => s.TenDangNhap == acc.TenDangNhap).FirstOrDefault();
                if (query != null)
                {
                    if (acc.MatKhau == query.MatKhau)
                    {
                        var role = _context.UserRoles.Where(x => x.UserId == query.UserId && x.RoleId == 2);
                        if (query != null)
                        {
                            ViewData["MUser"] = _context.Users.Where(x => x.UserId == query.UserId).FirstOrDefault();
                            HttpContext.Session.SetString("UserId", query.UserId.ToString());
                            var user = _context.Users.Where(x => x.UserId == query.UserId).FirstOrDefault().HoVaTen.Split(' ');
                            HttpContext.Session.SetString("UserName", user[user.Length - 1]);
                            return Redirect("/Home");
                        }
                    }
                    else
                    {
                        ViewData["ErrorModel"] = "Tên đăng nhập hoặc mật khẩu không đúng!";
                        return View();
                    }    
                }
                else
                {
                    ViewData["ErrorModel"] = "Tên đăng nhập hoặc mật khẩu không đúng!";
                    return View();
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Home");
        }

        public IActionResult Signup()
        {
            return View();
        }

        public IActionResult Account()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return Redirect("/Home/Error");
            }
            return View();
        }

        public IActionResult ThuongHieu(int id)
        {
            ViewData["TenThuongHieu"] = _context.ThuongHieus.Where(x => x.ThuongHieuId == id).First().TenThuongHieu;
            return View();
        }

        public IActionResult TatCaDienThoai()
        {
            var qrGetDienThoai = _context.DienThoais.ToList();
            return View(qrGetDienThoai);
        }

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
            var qrGetThuongHieu = _context.DienThoais.Include(x => x.ThuongHieu).Where(x => x.SoLuong > 0).Select(x => x.ThuongHieu).OrderBy(x=>x.TenThuongHieu).Distinct();
            return PartialView(qrGetThuongHieu.ToList());
        }
    }
}
