using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PTStore.Common.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("Tên đăng nhập"),Required]
        public string TenDangNhap { get; set; }

        [DisplayName("Mật khẩu"),Required]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
    }
}
