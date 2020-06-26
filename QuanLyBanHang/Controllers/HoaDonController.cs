using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public HoaDonController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        // GET: HoaDon
        public IActionResult Index()
        {
            List<PhiShip> phiship = _context.PhiShip.ToList();
            List<KhachHang> khachhang = _context.KhachHang.ToList();
            List<HoaDon> hoadon = _context.HoaDon.ToList();
            var thongtinhoadon = (from hd in hoadon
                                  join kh in khachhang on hd.KhachHangId equals kh.KhachHangId
                                  join ship in phiship on hd.PhiShipId equals ship.PhiShipId
                                  where hd.TrangThai == "Chờ xử lí"
                                  select new ThongTinHoaDon
                                  {
                                      HoaDon = hd,
                                      PhiShip = ship,
                                      KhachHang = kh
                                  });


            return View(thongtinhoadon);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var nhaCungCap = await _context.NhaCungCap.FirstOrDefaultAsync(m => m.NhaCungCapId == id);
            return View(nhaCungCap);
        }

        [HttpPost]
        public string Duyet(int id)
        {
            var hd = _context.HoaDon.Find(id);
            hd.TrangThai = "Đã xử lí";
            _context.Update(hd);
            _context.SaveChanges();
            string message = "Đã duyệt hóa đơn thành công";
            return message;
        }

        [HttpPost]
        public ActionResult DuyetSelected(int[] selected)
        {
            foreach (int id in selected)
            {
                var hd = _context.HoaDon.Find(id);
                hd.TrangThai = "Đã xử lí";
                _context.Update(hd);
            }
            _context.SaveChanges();
            return Json("Tất cả những hóa đơn được chọn đã duyệt thành công");
        }
    }

  
}
