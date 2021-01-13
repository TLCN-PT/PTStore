using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PTStore.Common.ViewModels
{
    public class SignupViewModel
    {
        [Required(ErrorMessage ="Họ và tên không được để trống!")]
        public string HoVaTen { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống!")]
        [MinLength(5,ErrorMessage ="Tên đăng nhập phải từ 5 kí tự trở lên!")]
        [MaxLength(20,ErrorMessage ="Tên đăng nhập không được dài hơn 20 kí tự!")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống!")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[a-z]).{5,}$",
            ErrorMessage = "Mật khẩu ít nhất 5 ký tự, phải bao gồm ký tự hoa, ký tự thường và số")]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Nhập lại mật khẩu không được để trống!")]
        [DataType(DataType.Password)]
        public string NhapLaiMatKhau { get; set; }

        [Required(ErrorMessage = "Email không được để trống!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [MinLength(10,ErrorMessage ="Vui lòng nhập chính xác số điện thoại!")]
        [MaxLength(10, ErrorMessage = "Vui lòng nhập chính xác số điện thoại!")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",ApplyFormatInEditMode = true)]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống!")]
        public string GioiTinh { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống!")]
        public string DiaChi { get; set; }
    }
}
