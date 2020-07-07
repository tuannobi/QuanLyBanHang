using System;
using System.Collections.Generic;

namespace QuanLyBanHang.Models
{
    public partial class ChiTietKhuyenMai
    {
        public int SanPhamId { get; set; }
        public int KhuyenMaiId { get; set; }
        public float PhanTramGiam { get; set; }

        public virtual KhuyenMai KhuyenMai { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
