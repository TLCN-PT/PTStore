using Microsoft.AspNetCore.Mvc;
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
            
            return Redirect("/QuenMatKhau/XacNhanMa");
        }

        public IActionResult XacNhanMa()
        {
            return View();
        }
    }
}
