using System;
using System.Collections.Generic;

#nullable disable

namespace PTStore.Models
{
    public partial class DienThoai
    {
        public DienThoai()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            ThongSoKyThuats = new HashSet<ThongSoKyThuat>();
        }

        public int DienThoaiId { get; set; }
        public string Name { get; set; }
        public string TinhTrang { get; set; }
        public string HinhAnh { get; set; }
        public double? Gia { get; set; }
        public double? GiaGoc { get; set; }
        public int? SoLuong { get; set; }
        public int? ThuongHieuId { get; set; }
        public int? ThongSoKyThuatId { get; set; }

        public virtual ThongSoKyThuat ThongSoKyThuat { get; set; }
        public virtual ThuongHieu ThuongHieu { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual ICollection<ThongSoKyThuat> ThongSoKyThuats { get; set; }
    }
}
