using System;
using System.Collections.Generic;

namespace QuanLyBanHang.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            PhieuNhap = new HashSet<PhieuNhap>();
        }

        public int NhaCungCapId { get; set; }
        public string TenNhaCungCap { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string Sdt { get; set; }

        public virtual ICollection<PhieuNhap> PhieuNhap { get; set; }
    }
}
