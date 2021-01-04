using Microsoft.AspNetCore.Mvc;
using PTStore.Common.ViewModels;
using PTStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PTStore.Common.MD5;
using Microsoft.AspNetCore.Http;

namespace PTStore.Controllers
{
    public class DangKyController : Controller
    {
        private readonly PTStoreContext context = new PTStoreContext();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return Redirect("/Home/Error");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(SignupViewModel acc)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (acc.MatKhau != acc.NhapLaiMatKhau)
            {
                ViewData["ModelError"] = "Mật khẩu và Nhập lại mật khẩu không giống nhau!";
                return View();
            }

            if (IsExistUsername(acc.TenDangNhap))
            {
                ViewData["ModelError"] = "Tên đăng nhập đã tồn tại!";
                return View();
            }

            if (IsExistEmail(acc.Email))
            {
                ViewData["ModelError"] = "Email đã tồn tại!";
                return View();
            }
            context.Accounts.Add(new Account
            {
                TenDangNhap = acc.TenDangNhap,
                MatKhau=GetMD5.GetHash(acc.MatKhau),
                TrangThai = "Enable",
                User = new User
                {
                    SoDienThoai = acc.SoDienThoai,
                    Email = acc.Email,
                    HoVaTen = acc.HoVaTen,
                    NgaySinh = acc.NgaySinh,
                    GioiTinh = acc.GioiTinh,
                    DiaChi = acc.DiaChi
                }
            });
            context.SaveChanges();

            var qrGetUser = context.Users.Where(x => x.Email == acc.Email).FirstOrDefault();
            qrGetUser.AccountId = context.Accounts.Where(x => x.TenDangNhap == acc.TenDangNhap).FirstOrDefault().AccountId;

            UserRole userRole = new UserRole();
            userRole.RoleId = 2;
            userRole.UserId = qrGetUser.UserId;
            context.UserRoles.Add(userRole);
            context.SaveChanges();
            HttpContext.Session.SetString("ThongBao", "Đăng ký thành công! Vui lòng đăng nhập!");
            return Redirect("/DangNhap");
        }

        public bool IsExistUsername(string username)
        {
            var qr = context.Accounts.Where(x => x.TenDangNhap == username).FirstOrDefault();
            if (qr == null)
                return false;
            return true;
        }

        public bool IsExistEmail(string email)
        {
            var qr = context.Users.Where(x => x.Email == email).FirstOrDefault();
            if (qr == null)
                return false;
            return true;
        }

    }
}
