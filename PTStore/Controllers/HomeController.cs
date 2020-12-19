﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PTStore.Common.ViewModels;
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

        private readonly PTStoreContext _context = new PTStoreContext();

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

        public IActionResult Login(LoginViewModel acc)
        {
            if (ModelState.IsValid)
            {
                var query = _context.Accounts.Where(s => s.TenDangNhap == acc.TenDangNhap).FirstOrDefault();
                if (query != null)
                {
                    if(acc.MatKhau == query.MatKhau)
                    {
                        var role = _context.UserRoles.Where(x => x.UserId == query.UserId && x.RoleId==2);
                        if(query!=null)
                        {
                            ViewData["MUser"] = _context.Users.Where(x => x.UserId == query.UserId).FirstOrDefault();
                            return Redirect("/Customer");
                        }
                    }
                }
            }
            return View();
        }
    }
}
