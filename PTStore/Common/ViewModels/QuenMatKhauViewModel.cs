using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PTStore.Common.ViewModels
{
    public class QuenMatKhauViewModel
    {
        public string MaXacNhan { get; set; }

        [RegularExpression(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[a-z]).{5,}$",
            ErrorMessage = "Mật khẩu ít nhất 5 ký tự, phải bao gồm ký tự hoa, ký tự thường và số")]
        public string MatKhau { get; set; }

        public string NhapLai { get; set; }
    }
}
