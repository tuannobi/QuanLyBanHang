using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanHang.Models
{
    public class ThongTinHoaDon
    {
        [Key]
        public int id { get; set; }
        public HoaDon HoaDon { get; set; }
        public KhachHang KhachHang { get; set; }
        public NhanVien NhanVien { get; set; }
        public PhiShip PhiShip { get; set; }
        public ChiTietHoaDon ChiTietHoaDon { get; set; }
        public SanPham SanPham { get; set; }
        public ChiTietKhuyenMai ChiTietKhuyenMai { get; set; }
    }
}
