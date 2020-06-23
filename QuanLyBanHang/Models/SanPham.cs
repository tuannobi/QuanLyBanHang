using System;
using System.Collections.Generic;

namespace QuanLyBanHang.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            BinhLuan = new HashSet<BinhLuan>();
            ChiTietHoaDon = new HashSet<ChiTietHoaDon>();
            ChiTietKhuyenMai = new HashSet<ChiTietKhuyenMai>();
            PhieuNhapSanPham = new HashSet<PhieuNhapSanPham>();
        }

        public int SanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public float? GiaBanLe { get; set; }
        public float? Kho { get; set; }
        public string HinhAnh { get; set; }
        public string TrangThai { get; set; }
        public int PhanLoaiId { get; set; }
        public string ThuongHieu { get; set; }
        public string MoTa { get; set; }

        public virtual PhanLoai PhanLoai { get; set; }
        public virtual ICollection<BinhLuan> BinhLuan { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMai { get; set; }
        public virtual ICollection<PhieuNhapSanPham> PhieuNhapSanPham { get; set; }
    }
}
