using Microsoft.AspNetCore.Mvc;
using PTStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        //private readonly PTStoreContext _context;
        public IActionResult Index()
        {
            return View();
        }
    }
}
