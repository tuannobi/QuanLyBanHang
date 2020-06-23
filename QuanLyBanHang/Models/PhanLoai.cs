using System;
using System.Collections.Generic;

namespace QuanLyBanHang.Models
{
    public partial class PhanLoai
    {
        public PhanLoai()
        {
            InverseNhomLoaiNavigation = new HashSet<PhanLoai>();
            SanPham = new HashSet<SanPham>();
        }

        public int PhanLoaiId { get; set; }
        public string Loai { get; set; }
        public string MoTa { get; set; }
        public int? NhomLoai { get; set; }

        public virtual PhanLoai NhomLoaiNavigation { get; set; }
        public virtual ICollection<PhanLoai> InverseNhomLoaiNavigation { get; set; }
        public virtual ICollection<SanPham> SanPham { get; set; }
    }
}
