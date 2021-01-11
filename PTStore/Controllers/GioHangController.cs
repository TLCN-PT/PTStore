using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTStore.Common.TienIch;
using PTStore.Common.ViewModels;
using PTStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTStore.Controllers
{
    public class GioHangController : Controller
    {
        public PTStoreContext context = new PTStoreContext();
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult ThemSanPham(int id)
        {
            var qr = context.DienThoais.Where(x => x.DienThoaiId == id).FirstOrDefault();
            if (qr == null)
                return Json("Lỗi!!!");

            if(GioHangViewModel.lstDienThoai != null)
            {
                if(GioHangViewModel.lstDienThoai.Find(x=>x.Id==id)!=null)
                {
                    GioHangViewModel.lstDienThoai.Find(x => x.Id == id).Soluong++;
                    GioHangViewModel.lstDienThoai.Find(x => x.Id == id).TongGia = 
                        GioHangViewModel.lstDienThoai.Find(x => x.Id == id).Soluong 
                        * GioHangViewModel.lstDienThoai.Find(x => x.Id == id).Gia;
                    GioHangViewModel.TongTien += GioHangViewModel.lstDienThoai.Find(x => x.Id == id).Gia;
                    return Json("Đã thêm " + qr.Name + " vào Giỏ hàng!");
                }    
            }    
            DienThoaiGioHang dt = new DienThoaiGioHang();
            dt.Id = qr.DienThoaiId;
            dt.Name = qr.Name;
            dt.Gia = qr.Gia.GetValueOrDefault();
            dt.Soluong = 1;
            dt.LinkHinh = qr.HinhAnh;
            dt.TongGia = qr.Gia.GetValueOrDefault();
            GioHangViewModel.lstDienThoai.Add(dt);
            GioHangViewModel.TongTien += dt.TongGia;

            return Json("Đã thêm " + qr.Name + " vào Giỏ hàng!");
        }

        public JsonResult TangSoLuong(int id)
        {
            GioHangViewModel.lstDienThoai.Find(x => x.Id == id).Soluong++;
            GioHangViewModel.lstDienThoai.Find(x => x.Id == id).TongGia =
                GioHangViewModel.lstDienThoai.Find(x => x.Id == id).Soluong
                * GioHangViewModel.lstDienThoai.Find(x => x.Id == id).Gia;
            GioHangViewModel.TongTien += GioHangViewModel.lstDienThoai.Find(x => x.Id == id).Gia;
            //string data = string.Format("0:0,0", GioHangViewModel.lstDienThoai.Find(x => x.Id == id).Gia) + "đ";
            return Json("");
        }

        public JsonResult GiamSoLuong(int id)
        {
            GioHangViewModel.lstDienThoai.Find(x => x.Id == id).Soluong--;
            GioHangViewModel.lstDienThoai.Find(x => x.Id == id).TongGia =
                GioHangViewModel.lstDienThoai.Find(x => x.Id == id).Soluong
                * GioHangViewModel.lstDienThoai.Find(x => x.Id == id).Gia;
            GioHangViewModel.TongTien -= GioHangViewModel.lstDienThoai.Find(x => x.Id == id).Gia;
            //string data = string.Format("0:0,0", GioHangViewModel.lstDienThoai.Find(x => x.Id == id).Gia) + "đ";
            return Json("");
        }

        public IActionResult XoaSP(int id)
        {
            GioHangViewModel.XoaSP(id);
            return Redirect("/GioHang");
        }

        public IActionResult TienHanhDatHang()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                HttpContext.Session.SetString("ThongBaoTuGioHang", "Bạn cần đăng nhập để đặt hàng!");
                return Redirect("/DangNhap");
            }
            return View();
        }

        public IActionResult DatHang()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DatHang(ThongTinThanhToanViewModel info)
        {
            if(info.HinhThucThanhToan == "TrucTiep")
            {
                string userid = HttpContext.Session.GetString("UserId");
                int id = int.Parse(userid);
                DonHang dh = new DonHang();
                var sdh = context.DonHangs.Where(x => x.UserId == id);
                int soluong = 1;
                if(sdh!=null)
                {
                    soluong = sdh.ToList().Count();
                }    
                DateTime dt = DateTime.Now;
                dh.MaDonHang = "D" + soluong.ToString() + "U" + userid + dt.AddHours(+7).ToString("ddMMyy");
                dh.UserId = id;
                dh.TenNguoiNhan = info.HoVaTen;
                dh.DiaChi = info.DiaChi;
                dh.Email = info.Email;
                dh.SoDienThoai = info.SoDienThoai;
                dh.NgayDatHang = dt.AddHours(+7);
                //Dự kiến giao sau 24h +7h theo múi giờ
                dh.NgayDuKienGiao = dt.AddHours(+31);
                dh.HinhThucThanhToan = "TrucTiep";
                dh.TrangTrai = "DatHangThanhCong";
                context.DonHangs.Add(dh);
                context.SaveChanges();
                
                var dhId = context.DonHangs.Where(x => x.MaDonHang == dh.MaDonHang).FirstOrDefault().DonHangId;

                foreach(var item in GioHangViewModel.lstDienThoai)
                {
                    context.ChiTietDonHangs.Add(new ChiTietDonHang
                    {
                        DonHangId = dhId,
                        DienThoaiId = item.Id,
                        SoLuong = item.Soluong,
                        Gia = item.Gia
                    });
                }
                context.SaveChanges();

                if (TIMail.GuiMailDatHangThanhCong(dh.Email))
                {

                };

            }
            return View();
        }
    }
}
