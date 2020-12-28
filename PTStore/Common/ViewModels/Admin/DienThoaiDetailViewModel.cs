using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PTStore.Models;
namespace PTStore.Common.ViewModels.Admin
{
    public class DienThoaiDetailViewModel
    {
        public DienThoai dienThoai { get; set; }
        public string TenThuongHieu { get; set; }
        public ThongSoKyThuat thongSoKyThuat { get; set; }
    }
}
