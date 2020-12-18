using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PTStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PTStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly PTStoreContext context = new PTStoreContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var s = context.DonHangs.Where(x => x.DonHangId == 11).FirstOrDefault();
            //ViewData["Image"] = s.Email;
            //ViewData["Name"] = s.NgayDatHang?.ToString("dd/M/yyyy");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Productbytype()
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
    }
}
