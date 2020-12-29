using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTStore.Controllers
{
    public class GioHangController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
