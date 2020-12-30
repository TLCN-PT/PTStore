using Microsoft.AspNetCore.Mvc;
using PTStore.Common.ViewModels;
using PTStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTStore.Controllers
{
    public class DangKyController : Controller
    {
        private readonly PTStoreContext context = new PTStoreContext();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SignupViewModel acc)
        {
            if(ModelState.IsValid)
            {
                var qr = context.Accounts.Where(x => x.TenDangNhap == acc.TenDangNhap);
                if(qr!=null)
                {
                    ViewData["ModelError"] = "Tên đăng nhập đã tồn tại!";
                }
                else
                {
                    ViewData["ModelError"] = "Tên đăng nhập chưa tồn tại!";
                }
            }
            return View();
        }

    }
}
