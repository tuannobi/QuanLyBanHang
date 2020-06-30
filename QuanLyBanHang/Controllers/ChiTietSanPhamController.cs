using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        private readonly QuanLyBanHangDbContext context;

        public ChiTietSanPhamController(QuanLyBanHangDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int? id)
        {
            var gioHangSession = HttpContext.Session.GetString("gioHangSession");
            int soLuong = 0;
            if (gioHangSession != null)
            {
                List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                soLuong = chiTietHoaDons.Count();
                ViewBag.chiTietHoaDons = chiTietHoaDons;
            }
            var sanPham = context.SanPham.Where(sp => sp.SanPhamId == id).FirstOrDefault();
            ViewBag.sanPham = sanPham;
            ViewBag.soLuong = soLuong;
            return View(sanPham);
        }
    }
}