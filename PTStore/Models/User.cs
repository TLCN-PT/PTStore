using System;
using System.Collections.Generic;

#nullable disable

namespace PTStore.Models
{
    public partial class User
    {
        public User()
        {
            Accounts = new HashSet<Account>();
            DonHangs = new HashSet<DonHang>();
            UserRoles = new HashSet<UserRole>();
        }

        public int UserId { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string HoVaTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public int? AccountId { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<DonHang> DonHangs { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
