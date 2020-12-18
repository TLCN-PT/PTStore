using System;
using System.Collections.Generic;

#nullable disable

namespace PTStore.Models
{
    public partial class ThongSoKyThuat
    {
        public ThongSoKyThuat()
        {
            DienThoais = new HashSet<DienThoai>();
        }

        public int ThongSoKyThuatId { get; set; }
        public string ManHinh { get; set; }
        public string HeDieuHanh { get; set; }
        public string CameraSau { get; set; }
        public string CameraTruoc { get; set; }
        public string Cpu { get; set; }
        public string Ram { get; set; }
        public string BoNhoTrong { get; set; }
        public string TheSim { get; set; }
        public string DungLuongPin { get; set; }
        public DateTime? NgayRaMat { get; set; }
        public int? DienThoaiId { get; set; }

        public virtual DienThoai DienThoai { get; set; }
        public virtual ICollection<DienThoai> DienThoais { get; set; }
    }
}
