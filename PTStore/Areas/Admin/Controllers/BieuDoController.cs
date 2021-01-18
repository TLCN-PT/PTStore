using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PTStore.Common.BieuDo;
using PTStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BieuDoController : Controller
    {
        private readonly PTStoreContext context = new PTStoreContext();
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")) || HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return Redirect("/Admin/Login");
            }
            List<DataPointCol> dataPoints1 = new List<DataPointCol>();

            List<string> thang = new List<string>();

            var dh = context.DonHangs.Include(x => x.ChiTietDonHangs).Where(x => x.TrangTrai == "GiaoThanhCong");
            int indexMonth = DateTime.Now.AddHours(7).Month;
            int indexYear = DateTime.Now.AddHours(7).Year;
            if (indexMonth == 1)
            {
                int count = 7;
                for (int i = 0; i < 6; i++)
                {
                    DataPointCol dp = new DataPointCol();
                    dp.Label = "Thang " + (count+i).ToString();
                    var qrr = context.DonHangs.Include(x => x.ChiTietDonHangs).Where(x => x.TrangTrai == "GiaoThanhCong").ToList();
                    foreach (var item in qrr)
                    {
                        if (item.NgayDatHang.Value.Month == count && item.NgayDatHang.Value.Year == indexYear - 1)
                        {
                            var chitiet = context.ChiTietDonHangs.Where(qr => qr.DonHangId == item.DonHangId).ToList();
                            foreach (var itemm in chitiet)
                            {
                                dp.Y += itemm.Gia;
                            }
                        }
                    }
                    dataPoints1.Add(dp);
                }
                ViewBag.DataPoints1 = dataPoints1.ToArray();
            }
            else if (indexMonth >= 7)
            {
                int count = indexMonth - 6;
                for (int i = 0; i < 6; i++)
                {
                    DataPointCol dp = new DataPointCol();
                    dp.Label = "Thang " + count.ToString();
                    var qrr = context.DonHangs.Include(x => x.ChiTietDonHangs).Where(x => x.TrangTrai == "GiaoThanhCong").ToList();
                    foreach (var item in qrr)
                    {
                        if (item.NgayDatHang.Value.Month == count && item.NgayDatHang.Value.Year == indexYear)
                        {
                            var chitiet = context.ChiTietDonHangs.Where(qr => qr.DonHangId == item.DonHangId).ToList();
                            foreach (var itemm in chitiet)
                            {
                                dp.Y += itemm.Gia;
                            }
                        }
                    }
                    dataPoints1.Add(dp);
                }
                ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
            }
            else if (indexMonth == 2)
            {
                int count = indexMonth + 2;
                for (int i = 0; i <= (12-count); i++)
                {
                    DataPointCol dp = new DataPointCol();
                    dp.Label = "Thang " + count.ToString();
                    var qrr = context.DonHangs.Include(x => x.ChiTietDonHangs).Where(x => x.TrangTrai == "GiaoThanhCong").ToList();
                    foreach (var item in qrr)
                    {
                        if (item.NgayDatHang.Value.Month == count && item.NgayDatHang.Value.Year == indexYear - 1)
                        {
                            var chitiet = context.ChiTietDonHangs.Where(qr => qr.DonHangId == item.DonHangId).ToList();
                            foreach (var itemm in chitiet)
                            {
                                dp.Y += itemm.Gia;
                            }
                        }
                    }
                    dataPoints1.Add(dp);
                }
                for (int j = 1; j < indexMonth; j++)
                {
                    DataPointCol dpp = new DataPointCol();
                    dpp.Label = "Thang " + count.ToString();
                    var qrr = context.DonHangs.Include(x => x.ChiTietDonHangs).Where(x => x.TrangTrai == "GiaoThanhCong").ToList();
                    foreach (var item in qrr)
                    {
                        if (item.NgayDatHang.Value.Month == count && item.NgayDatHang.Value.Year == indexYear)
                        {
                            var chitiet = context.ChiTietDonHangs.Where(qr => qr.DonHangId == item.DonHangId).ToList();
                            foreach (var itemm in chitiet)
                            {
                                dpp.Y += itemm.Gia;
                            }
                        }
                    }
                    dataPoints1.Add(dpp);
                }
                ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
            }
            else if (indexMonth == 3)
            {

            }
            else if (indexMonth == 4)
            {

            }
            else if (indexMonth == 5)
            {

            }
            else
            {

            }
            //using(PTStoreContext context = new PTStoreContext())
            //{

            //}    
            return View();
        }
    }
}
