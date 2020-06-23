using System;
using System.Collections.Generic;

namespace QuanLyBanHang.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            BinhLuan = new HashSet<BinhLuan>();
            HoaDon = new HashSet<HoaDon>();
            Reply = new HashSet<Reply>();
        }

        public int KhachHangId { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string Email { get; set; }
        public string Sdt { get; set; }
        public string DiaChi { get; set; }
        public int? TaiKhoanId { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
        public virtual ICollection<BinhLuan> BinhLuan { get; set; }
        public virtual ICollection<HoaDon> HoaDon { get; set; }
        public virtual ICollection<Reply> Reply { get; set; }
    }
}
