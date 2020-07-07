using System;
using System.Collections.Generic;

namespace QuanLyBanHang.Models
{
    public partial class PhieuNhapSanPham
    {
        public int PhieuNhapId { get; set; }
        public int SanPhamId { get; set; }
        public float GiaGoc { get; set; }
        public int SoLuong { get; set; }
        public float TongTien { get; set; }

        public virtual PhieuNhap PhieuNhap { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
