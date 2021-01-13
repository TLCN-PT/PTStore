using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BieuDoController : Controller
    {
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")) || HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Redirect("/Admin/Login");
            }
            return View();
        }
    }
}
