using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
            
        }
        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult Account()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult Productbytype()
        {
            return View();
        }
        public IActionResult Manageorder()
        {
            return View();
        }
        public IActionResult Feedback()
        {
            return View();
        }
    }
}
