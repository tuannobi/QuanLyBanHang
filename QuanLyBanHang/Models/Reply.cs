using System;
using System.Collections.Generic;

namespace QuanLyBanHang.Models
{
    public partial class Reply
    {
        public int ReplyId { get; set; }
        public int BinhLuanId { get; set; }
        public int KhachHangId { get; set; }
        public DateTime? ThoiGian { get; set; }
        public string NoiDung { get; set; }

        public virtual BinhLuan BinhLuan { get; set; }
        public virtual KhachHang KhachHang { get; set; }
    }
}
