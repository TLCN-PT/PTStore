using System;
using System.Collections.Generic;

#nullable disable

namespace PTStore.Models
{
    public partial class DonHang
    {
        public DonHang()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        public int DonHangId { get; set; }
        public string MaDonHang { get; set; }
        public int? UserId { get; set; }
        public string TenNguoiNhan { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime? NgayDatHang { get; set; }
        public DateTime? NgayDuKienGiao { get; set; }
        public string HinhThucThanhToan { get; set; }
        public string TrangTrai { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
