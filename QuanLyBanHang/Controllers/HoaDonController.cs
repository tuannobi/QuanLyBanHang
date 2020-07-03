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
            var thongtinhoadon = (from hd in _context.HoaDon
                                 join kh in _context.KhachHang on hd.KhachHangId equals kh.KhachHangId
                                 join ship in _context.PhiShip on hd.PhiShipId equals ship.PhiShipId
                                 where hd.TrangThai == "Chờ xử lý"
                                 select new ThongTinHoaDon
                                  {
                                      HoaDon = hd,
                                      PhiShip = ship,
                                      KhachHang = kh
                                  });
            return View(thongtinhoadon);
        }

        public IActionResult Index1()
        {
            var thongtinhoadon = (from hd in _context.HoaDon
                                  join kh in _context.KhachHang on hd.KhachHangId equals kh.KhachHangId
                                  join ship in _context.PhiShip on hd.PhiShipId equals ship.PhiShipId
                                  where hd.TrangThai == "Đã xử lý"
                                  select new ThongTinHoaDon
                                  {
                                      HoaDon = hd,
                                      PhiShip = ship,
                                      KhachHang = kh
                                  });
            return View(thongtinhoadon);
        }

        public IActionResult Details(int id)
        {
            var thongtinsanpham = (from sp in _context.SanPham
                                   join cthd in _context.ChiTietHoaDon on sp.SanPhamId equals cthd.SanPhamId
                                   where cthd.HoaDonId == id
                                   select new ThongTinHoaDon
                                   {
                                       SanPham = sp,
                                       ChiTietHoaDon = cthd
                                   });
            foreach (var item in thongtinsanpham)
            {
                Console.WriteLine(item.SanPham.SanPhamId);
            }
            var thongtinhoadon = (from hd in _context.HoaDon
                                  join kh in _context.KhachHang on hd.KhachHangId equals kh.KhachHangId
                                  join ship in _context.PhiShip on hd.PhiShipId equals ship.PhiShipId
                                  where hd.HoaDonId == id
                                  select new ThongTinHoaDon
                                  {
                                      HoaDon = hd,
                                      PhiShip = ship,
                                      KhachHang = kh
                                  });
            ViewBag.TTHD = thongtinhoadon;
            ViewBag.TTSP = thongtinsanpham;
            return View();
        }

        [HttpPost]
        public ActionResult Duyet(int id)
        {
            var hd = _context.HoaDon.Find(id);
            hd.TrangThai = "Đã xử lý";
            hd.ThoiGianDaXuLy = DateTime.Now;
            _context.Update(hd);
            _context.SaveChanges();
            return Json("Hóa đơn này đã duyệt thành công");
        }

        [HttpPost]
        public ActionResult DuyetSelected(int[] selected)
        {
            foreach (int id in selected)
            {
                var hd = _context.HoaDon.Find(id);
                hd.TrangThai = "Đã xử lý";
                hd.ThoiGianDaXuLy = DateTime.Now;
                _context.Update(hd);
            }
            _context.SaveChanges();
            return Json("Tất cả những hóa đơn được chọn đã duyệt thành công");
        }
    }

  
}
