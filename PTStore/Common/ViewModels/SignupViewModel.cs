using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PTStore.Common.ViewModels
{
    public class SignupViewModel
    {
        [Required]
        public string HoVaTen { get; set; }

        [Required]
        [MinLength(5,ErrorMessage ="Tên đăng nhập phải từ 5 kí tự trở lên!")]
        [MaxLength(20,ErrorMessage ="Tên đăng nhập không được dài hơn 50 kí tự!")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[a-z]).{5,}$",
            ErrorMessage = "Mật khẩu ít nhất 8 ký tự, phải bao gồm ký tự hoa, ký tự thường và số")]
        public string TenDangNhap { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NhapLaiMatKhau { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [MinLength(10,ErrorMessage ="Vui lòng nhập chính xác số điện thoại!")]
        public string SoDienThoai { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }

        [Required]
        public string GioiTinh { get; set; }

        [Required]
        public string DiaChi { get; set; }
    }
}
