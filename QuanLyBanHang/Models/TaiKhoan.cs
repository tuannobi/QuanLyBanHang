using System;
using System.Collections.Generic;

namespace QuanLyBanHang.Models
{
    public partial class TaiKhoan
    {
        public TaiKhoan()
        {
            Admin = new HashSet<Admin>();
            KhachHang = new HashSet<KhachHang>();
            NhanVien = new HashSet<NhanVien>();
        }

        public int TaiKhoanId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? NgayTao { get; set; }
        public int VaiTroId { get; set; }

        public virtual VaiTro VaiTro { get; set; }
        public virtual ICollection<Admin> Admin { get; set; }
        public virtual ICollection<KhachHang> KhachHang { get; set; }
        public virtual ICollection<NhanVien> NhanVien { get; set; }
    }
}
