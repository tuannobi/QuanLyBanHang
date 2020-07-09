﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLyBanHang.Models;
using Rotativa.AspNetCore;

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
            var thongtinhoadon = _context.HoaDon.Include("KhachHang").Include("PhiShip").Where(hd => hd.TrangThai == "Chờ xử lý");
            /*var thongtinhoadon = (from hd in _context.HoaDon
                                 join kh in _context.KhachHang on hd.KhachHangId equals kh.KhachHangId
                                 join ship in _context.PhiShip on hd.PhiShipId equals ship.PhiShipId
                                 where hd.TrangThai == "Chờ xử lý"
                                 select new ThongTinHoaDon
                                  {
                                      HoaDon = hd,
                                      PhiShip = ship,
                                      KhachHang = kh
                                  });*/
            ViewBag.TTHD = thongtinhoadon;
            return View();
        }

        public IActionResult Index1()
        {
            var thongtinhoadon = _context.HoaDon.Include("KhachHang").Include("PhiShip").Where(hd => hd.TrangThai == "Đã xử lý");
            /*var thongtinhoadon = (from hd in _context.HoaDon
                                  join kh in _context.KhachHang on hd.KhachHangId equals kh.KhachHangId
                                  join ship in _context.PhiShip on hd.PhiShipId equals ship.PhiShipId
                                  where hd.TrangThai == "Đã xử lý"
                                  select new ThongTinHoaDon
                                  {
                                      HoaDon = hd,
                                      PhiShip = ship,
                                      KhachHang = kh
                                  });*/
            ViewBag.TTHD = thongtinhoadon;
            return View();
        }

        public IActionResult Details(int id)
        {
            var thongtinsanpham = _context.ChiTietHoaDon.Include("SanPham").Where(sp => sp.HoaDonId == id);
            /*var thongtinsanpham = (from sp in _context.SanPham
                                  join cthd in _context.ChiTietHoaDon on sp.SanPhamId equals cthd.SanPhamId
                                  where cthd.HoaDonId == id
                                  select new ThongTinHoaDon
                                  {
                                      SanPham = sp,
                                      ChiTietHoaDon = cthd
                                  });*/
            var thongtinhoadon = _context.HoaDon.Include("KhachHang").Include("PhiShip").Where(hd => hd.HoaDonId == id);
            /*var thongtinhoadon = (from hd in _context.HoaDon
                                  join kh in _context.KhachHang on hd.KhachHangId equals kh.KhachHangId
                                  join ship in _context.PhiShip on hd.PhiShipId equals ship.PhiShipId
                                  where hd.HoaDonId == id
                                  select new ThongTinHoaDon
                                  {
                                      HoaDon = hd,
                                      PhiShip = ship,
                                      KhachHang = kh
                                  });*/
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

        [HttpGet]
        public IActionResult ThongKeHoaDonTheoThoiGian()
        {
            return View("ThongKeHoaDonTheoThoiGian");
        }

        [HttpPost]
        public IActionResult ThongKeHoaDonTheoThoiGian(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            Console.WriteLine(ngayBatDau);
            Console.WriteLine(ngayKetThuc);
            List<HoaDon> hoaDons = _context.HoaDon.Where(hd => hd.ThoiGianDaXuLy >= ngayBatDau && hd.ThoiGianDaXuLy <= ngayKetThuc).ToList();
            ViewBag.TTHD = hoaDons;
            HttpContext.Session.SetString("ngayBatDauSession", JsonConvert.SerializeObject(ngayBatDau));
            HttpContext.Session.SetString("ngayKetThucSession", JsonConvert.SerializeObject(ngayKetThuc));
            return View("ThongKeHoaDonTheoThoiGian");
        }

        public IActionResult InDanhSachHoaDon()
        {
            DateTime ngayBatDau = JsonConvert.DeserializeObject<DateTime>(HttpContext.Session.GetString("ngayBatDauSession"));
            DateTime ngayKetThuc = JsonConvert.DeserializeObject<DateTime>(HttpContext.Session.GetString("ngayKetThucSession"));
            List<HoaDon> hoaDons = _context.HoaDon.Where(hd => hd.ThoiGianDaXuLy >= ngayBatDau && hd.ThoiGianDaXuLy <= ngayKetThuc).ToList();
            //
            return View("Index");
        }
    
    }

   


}
