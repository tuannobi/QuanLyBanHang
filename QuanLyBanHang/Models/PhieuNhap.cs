using System;
using System.Collections.Generic;

namespace QuanLyBanHang.Models
{
    public partial class PhieuNhap
    {
        public PhieuNhap()
        {
            PhieuNhapSanPham = new HashSet<PhieuNhapSanPham>();
        }

        public int PhieuNhapId { get; set; }
        public int? NhaCungCapId { get; set; }
        public DateTime? NgayTao { get; set; }
        public float? TongTien { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }
        public virtual ICollection<PhieuNhapSanPham> PhieuNhapSanPham { get; set; }
    }
}
