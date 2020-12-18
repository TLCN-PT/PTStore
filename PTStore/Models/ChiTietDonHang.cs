using System;
using System.Collections.Generic;

#nullable disable

namespace PTStore.Models
{
    public partial class ChiTietDonHang
    {
        public int DonHangId { get; set; }
        public int DienThoaiId { get; set; }
        public int? SoLuong { get; set; }
        public double? Gia { get; set; }

        public virtual DienThoai DienThoai { get; set; }
        public virtual DonHang DonHang { get; set; }
    }
}
