using System;
using System.Collections.Generic;

namespace QuanLyBanHang.Models
{
    public partial class ChiTietHoaDon
    {
        public int HoaDonId { get; set; }
        public int SanPhamId { get; set; }
        public int? SoLuong { get; set; }
        public float? TienKhuyenMai { get; set; }
        public float? TongTien { get; set; }

        public virtual HoaDon HoaDon { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
