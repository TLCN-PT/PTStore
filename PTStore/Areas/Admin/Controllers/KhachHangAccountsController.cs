﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PTStore.Models;

namespace PTStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KhachHangAccountsController : Controller
    {
        private readonly PTStoreContext _context;

        public KhachHangAccountsController(PTStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/KhachHangAccounts
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")) || HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Redirect("/Admin/Login");
            }
            var query = (from acc in _context.Accounts
                         join usr in _context.UserRoles
             on acc.AccountId equals usr.UserId
                         where usr.RoleId == 2
                         select acc);
            query = query.Include(x => x.User);
            //into grouping;
            //            select new { acc, acc.User, usr = grouping.Where(x => x.RoleId == 1).ToList() };
            //List<Account> accounts = query;
            //return View(query);
            return View(await query.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string search)
        {
            var query = (from acc in _context.Accounts
                         join usr in _context.UserRoles
             on acc.AccountId equals usr.UserId
                         where usr.RoleId == 2
                         select acc);
            query = query.Include(x => x.User);
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.TenDangNhap.Contains(search) || x.UserId == int.Parse(search));
                
            }
            //into grouping;
            //            select new { acc, acc.User, usr = grouping.Where(x => x.RoleId == 1).ToList() };
            //List<Account> accounts = query;
            //return View(query);
            return View(await query.ToListAsync());
        }
        // GET: Admin/KhachHangAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Admin/KhachHangAccounts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Admin/KhachHangAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,TenDangNhap,MatKhau,TrangThai,UserId")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", account.UserId);
            return View(account);
        }

        // GET: Admin/KhachHangAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", account.UserId);
            return View(account);
        }

        // POST: Admin/KhachHangAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,TenDangNhap,MatKhau,TrangThai,UserId")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var qr = _context.Accounts.Where(x=>x.AccountId==id).FirstOrDefault();
                    qr.TrangThai = account.TrangThai;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", account.UserId);
            
            return View(account);
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}
