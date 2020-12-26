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
            if (IsCustomerLogged())
            {
                HttpContext.Session.Clear();
                return Redirect("/Home");
            }
            return Redirect("/Home/Error");
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(SignupViewModel suMd)
        {
            if(ModelState.IsValid)
            {
                ViewData["ModelError"] = "Nhập đúng";
            }
            else
                ViewData["ModelError"] = "Nhập sai";
            return View();
        }

        public IActionResult Account()
        {
            if(IsCustomerLogged())
            {
                return View();
            }
            return Redirect("/Home/Error");
        }

        public IActionResult DienThoai(string sortOrder, int thuonghieuOrder)
        {
            if(string.IsNullOrEmpty(sortOrder))
            {
                ViewData["sortOrderTang"] = "GiaThapToiCao";
                ViewData["sortOrderGiam"] = "GiaCaoXuongThap";
            }    
            else if(sortOrder == "GiaCaoXuongThap")
            {
                ViewData["sortOrderGiam"] = "";
                ViewData["sortOrderTang"] = "GiaThapToiCao";
            }
            else
            {
                ViewData["sortOrderTang"] = "";
                ViewData["sortOrderGiam"] = "GiaCaoXuongThap";
            }
            
            if(thuonghieuOrder>0)
            {
                ViewData["thuonghieuOrder"] = thuonghieuOrder;
            }
            DienThoaiViewModel dt = new DienThoaiViewModel();
            switch (sortOrder)
            {
                case "GiaCaoXuongThap":
                    dt.lstDienThoai = _context.DienThoais.Where(x => x.SoLuong > 0).OrderByDescending(s => s.Gia).ToList();
                    break;
                case "GiaThapToiCao":
                    dt.lstDienThoai = _context.DienThoais.Where(x => x.SoLuong > 0).OrderBy(s => s.Gia).ToList();
                    break;
                default:
                    dt.lstDienThoai = dt.lstDienThoai = _context.DienThoais.Where(x => x.SoLuong > 0).OrderBy(s => s.DienThoaiId).ToList();
                    break;
            }
            if(thuonghieuOrder>0)
            {
                dt.lstDienThoai = dt.lstDienThoai.FindAll(x => x.ThuongHieuId == thuonghieuOrder);
            }
            dt.lstThuongHieu = _context.ThuongHieus.ToList();
            return View(dt);
        }

        public IActionResult TatCaDienThoaiPartial(string sortOrder)
        {
            var qrGetDienThoai = _context.DienThoais.Where(x => x.SoLuong > 0);
            switch (sortOrder)
            {
                case "giagiamdan":
                    qrGetDienThoai = qrGetDienThoai.OrderByDescending(s => s.Gia);
                    break;
                case "giatangdan":
                    qrGetDienThoai = qrGetDienThoai.OrderBy(s => s.Gia);
                    break;
                default:
                    qrGetDienThoai = qrGetDienThoai.OrderBy(s => s.DienThoaiId);
                    break;
            }
            return PartialView(qrGetDienThoai.AsNoTracking().ToList());
        }

        public IActionResult DienThoaiPartial(string sortOrder)
        {
            var qrGetDienThoai = _context.DienThoais.Where(x=>x.SoLuong>0);
            return View(qrGetDienThoai.AsNoTracking().ToList());
        }

        [HttpGet]
        public IActionResult ThuongHieu()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ThuongHieu(int id)
        {
            ViewData["TenThuongHieu"] = _context.ThuongHieus.Where(x => x.ThuongHieuId == id).First().TenThuongHieu;
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
            var qrGetThuongHieu = _context.DienThoais.Include(x => x.ThuongHieu).Where(x => x.SoLuong > 0).Select(x => x.ThuongHieu).OrderBy(x=>x.TenThuongHieu).Distinct();
            return PartialView(qrGetThuongHieu.ToList());
        }

        public IActionResult FooterPartial()
        {
            var qrGetThuongHieu = _context.DienThoais.Include(x => x.ThuongHieu).Where(x => x.SoLuong > 0).Select(x => x.ThuongHieu).OrderBy(x => x.TenThuongHieu).Distinct();
            return PartialView(qrGetThuongHieu.ToList());
        }

        // Error Message
        public IActionResult ErrorMessage()
        {
            return View();
        }
    }
}
