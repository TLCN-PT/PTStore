using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTStore.Common.MD5;
using PTStore.Common.TienIch;
using PTStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTStore.Controllers
{
    public class QuenMatKhauController : Controller
    {
        private readonly PTStoreContext context = new PTStoreContext();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email)
        {
            var qr = context.Users.Where(x => x.Email == email).FirstOrDefault();
            if(qr==null)
            {
                ViewData["Message"] = "Email chưa được đăng ký!";
                return View();
            }
            Random rd = new Random();
            string x = rd.Next(10000, 99999).ToString();
            HttpContext.Session.SetString("MaXacNhan", x);
            TIMail.GuiMailMaXacNhan(email, x);
            HttpContext.Session.SetString("email", email);
            return Redirect("/QuenMatKhau/XacNhanMa");
        }

        public IActionResult XacNhanMa()
        {
            return View();
        }

        [HttpPost]
        public IActionResult XacNhanMa(string maxacnhan, string matkhaumoi, string nhaplai)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("MaXacNhan")))
                return Redirect("/Home/Error");

            if (!ModelState.IsValid)
            {
                ViewData["ms"] = "Mật khẩu không hợp lệ!";
                return View();
            }    

            if(maxacnhan != HttpContext.Session.GetString("MaXacNhan"))
            {
                ViewData["ms"] = "Mã xác nhận không chính xác!";
                return View();
            }
            if(matkhaumoi != nhaplai)
            {
                ViewData["ms"] = "Mật khẩu nhập lại không đúng!";
                return View();
            }
            string email = HttpContext.Session.GetString("email");
            var qr = context.Users.Include(x => x.Account).Where(x => x.Email == email).FirstOrDefault();
            qr.Account.MatKhau = GetMD5.GetHash(matkhaumoi);
            context.SaveChanges();
            HttpContext.Session.SetString("ThongBaoTuQuenMatKhau", "Đổi mật khẩu thành công! Vui lòng đăng nhập!");
            return Redirect("/DangNhap");
        }
    }
}
