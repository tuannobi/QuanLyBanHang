using System;
using System.Collections.Generic;

namespace QuanLyBanHang.Models
{
    public partial class KhuyenMai
    {
        public KhuyenMai()
        {
            ChiTietKhuyenMai = new HashSet<ChiTietKhuyenMai>();
        }

        public int KhuyenMaiId { get; set; }
        public string TenKhuyenMai { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMai { get; set; }
    }
}
