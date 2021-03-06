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
using PTStore.Common.TienIch;

namespace PTStore.Controllers
{
    public class DienThoaiController : Controller
    {
        private readonly PTStoreContext _context = new PTStoreContext();
        public IActionResult Index(string sortOrder, int thuonghieuOrder, string searchOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                ViewData["sortOrderTang"] = "GiaThapToiCao";
                ViewData["sortOrderGiam"] = "GiaCaoXuongThap";
            }
            else if (sortOrder == "GiaCaoXuongThap")
            {
                ViewData["sortOrderGiam"] = "";
                ViewData["sortOrderTang"] = "GiaThapToiCao";
            }
            else
            {
                ViewData["sortOrderTang"] = "";
                ViewData["sortOrderGiam"] = "GiaCaoXuongThap";
            }

            if (thuonghieuOrder > 0)
            {
                ViewData["thuonghieuOrder"] = thuonghieuOrder;
            }
            DienThoaiViewModel dt = new DienThoaiViewModel();
            switch (sortOrder)
            {
                case "GiaCaoXuongThap":
                    dt.lstDienThoai = _context.DienThoais.Include(x=>x.ThongSoKyThuat).Where(x => x.SoLuong > 0).Where(x=>x.TinhTrang=="DangKinhDoanh").OrderByDescending(s => s.Gia).ToList();
                    break;
                case "GiaThapToiCao":
                    dt.lstDienThoai = _context.DienThoais.Include(x => x.ThongSoKyThuat).Where(x => x.SoLuong > 0).Where(x => x.TinhTrang == "DangKinhDoanh").OrderBy(s => s.Gia).ToList();
                    break;
                default:
                    dt.lstDienThoai = dt.lstDienThoai = _context.DienThoais.Include(x => x.ThongSoKyThuat).Where(x => x.TinhTrang == "DangKinhDoanh").Where(x => x.SoLuong > 0).OrderBy(s => s.DienThoaiId).ToList();
                    break;
            }
            if (thuonghieuOrder > 0)
            {
                dt.lstDienThoai = dt.lstDienThoai.FindAll(x => x.ThuongHieuId == thuonghieuOrder);
            }
            if(searchOrder!=null)
            {
                dt.lstDienThoai = dt.lstDienThoai.Where(x => x.Name.ToUpper().Contains(searchOrder.ToUpper())).ToList();
            }    
            dt.lstThuongHieu = _context.ThuongHieus.ToList();
            return View(dt);
        }

        public IActionResult ChiTiet(int id)
        {
            TIMail.idienthoai = id;
            var qr = _context.DienThoais.Include(d=>d.ThongSoKyThuat).Where(x => x.DienThoaiId == id);
            if (qr == null)
                return Redirect("Home/Error");
            return View(qr.First());
        }
    }
}
