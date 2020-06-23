using System;
using System.Collections.Generic;

namespace QuanLyBanHang.Models
{
    public partial class PhiShip
    {
        public PhiShip()
        {
            HoaDon = new HashSet<HoaDon>();
        }

        public int PhiShipId { get; set; }
        public string Quan { get; set; }
        public float? ChiPhi { get; set; }

        public virtual ICollection<HoaDon> HoaDon { get; set; }
    }
}
