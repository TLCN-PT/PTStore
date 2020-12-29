using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PTStore.Models;
using PTStore.Common.ViewModels;
using Microsoft.AspNetCore.Http;

namespace PTStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
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
                        var role = _context.UserRoles.Where(x => x.UserId == query.UserId).FirstOrDefault().RoleId;
                        if (role==1)
                        {
                            HttpContext.Session.Clear();
                            HttpContext.Session.SetString("UserId", query.UserId.ToString());
                            var user = _context.Users.Where(x => x.UserId == query.UserId).FirstOrDefault().HoVaTen.Split(' ');
                            HttpContext.Session.SetString("UserName", user[user.Length - 1]);
                            HttpContext.Session.SetString("UserRole", "Admin");
                            return Redirect("/Admin/Home");
                        }
                        else
                        {
                            ViewData["ErrorModel"] = "Bạn không có quyền truy cập vào trang này!";
                            return View();
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
