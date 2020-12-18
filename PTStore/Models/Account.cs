using System;
using System.Collections.Generic;

#nullable disable

namespace PTStore.Models
{
    public partial class Account
    {
        public Account()
        {
            Users = new HashSet<User>();
        }

        public int AccountId { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string TrangThai { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
