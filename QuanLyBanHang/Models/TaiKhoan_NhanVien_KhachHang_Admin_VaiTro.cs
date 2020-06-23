using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanHang.Models
{
    public class TaiKhoan_NhanVien_KhachHang_Admin_VaiTro
    {
        

        [Key]
        public int id { get; set; }
        public TaiKhoan taiKhoan { get; set; }
        public VaiTro vaiTro { get; set; }
        public NhanVien nhanVien { get; set; }
        public KhachHang khachHang { get; set; }
        public KhachHang khachhang { get; internal set; }
        public Admin admin { get; set; }

    }
}
