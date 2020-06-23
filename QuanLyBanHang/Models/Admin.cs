using System;
using System.Collections.Generic;

namespace QuanLyBanHang.Models
{
    public partial class Admin
    {
        public int Adminid { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string Email { get; set; }
        public string Sdt { get; set; }
        public string DiaChi { get; set; }
        public int TaiKhoanId { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
