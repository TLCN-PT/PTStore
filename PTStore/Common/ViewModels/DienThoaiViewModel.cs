using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PTStore.Models;

namespace PTStore.Common.ViewModels
{
    public class DienThoaiViewModel
    {
        public List<ThuongHieu> lstThuongHieu { get; set; }
        public List<DienThoai> lstDienThoai { get; set; }
    }
}
