using System;
using System.Collections.Generic;

namespace QuanLyBanHang.Models
{
    public partial class VaiTro
    {
        public VaiTro()
        {
            TaiKhoan = new HashSet<TaiKhoan>();
        }

        public int VaiTroId { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoan { get; set; }
    }
}
