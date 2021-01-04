using PTStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PTStore.Common.ViewModels
{
    public static class GioHangViewModel
    {
        public static List<DienThoaiGioHang> lstDienThoai { get; set; } = new List<DienThoaiGioHang>();
        public static double TongTien { get; set; } = 0;

        public static void XoaGioHang()
        {
            lstDienThoai.Clear();
            TongTien = 0;
        }
    }

    public class DienThoaiGioHang
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Gia { get; set; }
        public string LinkHinh { get; set; }
        public int Soluong { get; set; }
        public double TongGia { get; set; }
    }

    public class ThongTinThanhToanViewModel
    {
        public string HoVaTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string HinhThucThanhToan { get; set; }
    }
}
