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
using Microsoft.AspNetCore.Http;

namespace PTStore.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly PTStoreContext _context = new PTStoreContext();
        public IActionResult Index()
        {
            if (IsCustomerLogged())
            {
                return View();
            }
            return Redirect("/Home/Error");
        }

        public IActionResult DangXuat()
        {
            if (IsCustomerLogged())
            {
                HttpContext.Session.Clear();
                return Redirect("/Home");
            }
            return Redirect("/Home/Error");
        }

        public bool IsCustomerLogged()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return false;
            }
            return true;
        }
    }
}