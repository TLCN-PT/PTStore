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
            //css them vo css co san luon ha
            //tao thu muc moi
            //roi them vo
            //sau khi chinh xon
            //chay OK -> change
            return View();
            
        }
    }
}
