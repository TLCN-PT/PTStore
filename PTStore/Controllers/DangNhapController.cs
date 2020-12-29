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
    public class DangNhapController : Controller
    {
        private readonly PTStoreContext _context = new PTStoreContext();
        public IActionResult Index()
        {
            ViewData["ErrorModel"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel acc)
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
                            HttpContext.Session.Clear();
                            HttpContext.Session.SetString("UserId", query.UserId.ToString());
                            var user = _context.Users.Where(x => x.UserId == query.UserId).FirstOrDefault().HoVaTen.Split(' ');
                            HttpContext.Session.SetString("UserName", user[user.Length - 1]);
                            HttpContext.Session.SetString("UserRole", "Customer");
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
    }
}
