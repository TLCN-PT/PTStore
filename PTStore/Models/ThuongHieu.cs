using System;
using System.Collections.Generic;

#nullable disable

namespace PTStore.Models
{
    public partial class ThuongHieu
    {
        public ThuongHieu()
        {
            DienThoais = new HashSet<DienThoai>();
        }

        public int ThuongHieuId { get; set; }
        public string TenThuongHieu { get; set; }
        public string HinhAnhThuongHieu { get; set; }

        public virtual ICollection<DienThoai> DienThoais { get; set; }
    }
}
