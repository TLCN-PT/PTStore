using Microsoft.AspNetCore.Mvc;
using PTStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PTStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        //private readonly PTStoreContext _context;
        public IActionResult Index()
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")) || HttpContext.Session.GetString("UserRole") != "Admin")
            //{
            //    return Redirect("/Admin/Login");
            //}
            return View();
        }
    }
}
