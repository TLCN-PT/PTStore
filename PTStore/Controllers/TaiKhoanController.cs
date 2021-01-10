using Microsoft.AspNetCore.Mvc;
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
                int id = int.Parse(HttpContext.Session.GetString("UserId"));
                var qr = _context.Users.Include(x => x.Account).Where(x => x.UserId == id).FirstOrDefault();
                return View(qr);
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

        public IActionResult LichSuMuaHang()
        {
            int id = int.Parse(HttpContext.Session.GetString("UserId"));
            var donHang = _context.DonHangs.Where(x => x.UserId == id).Include(x => x.ChiTietDonHangs).ThenInclude(x=>x.DienThoai).ToList();
            foreach (var item in donHang)
            {
                switch (item.TrangTrai)
                {
                    case "DaGiaoHang":
                        item.TrangTrai = "Đã giao hàng";
                        break;
                    case "DatHangThanhCong":
                        item.TrangTrai = "Đặt hàng thành công";
                        break;
                    case "GiaoThanhCong":
                        item.TrangTrai = "Giao thành công";
                        break;
                    case "BiHuy":
                        item.TrangTrai = "Bị huỷ";
                        break;
                    default:
                        item.TrangTrai = "Lỗi";
                        break;
                }
            }
            
            return View(donHang);
        }

        public IActionResult PhanHoi()
        {
            return View();
        }
    }
}
