using System;
using System.Collections.Generic;

namespace QuanLyBanHang.Models
{
    public partial class BinhLuan
    {
        public BinhLuan()
        {
            Reply = new HashSet<Reply>();
        }

        public int BinhLuanId { get; set; }
        public int KhachHangId { get; set; }
        public int SanPhamId { get; set; }
        public DateTime ThoiGian { get; set; }
        public string NoiDung { get; set; }

        public virtual KhachHang KhachHang { get; set; }
        public virtual SanPham SanPham { get; set; }
        public virtual ICollection<Reply> Reply { get; set; }
    }
}
