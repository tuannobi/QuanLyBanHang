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
    public class RegisterController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public RegisterController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var sessionUser = HttpContext.Session.GetString("sessionUser");
            var gioHangSession = HttpContext.Session.GetString("gioHangSession");
            int soLuong = 0;
            if (sessionUser != null)
            {
                return Redirect("/Homepage");
            }
            else
            {
                if (gioHangSession != null)
                {
                    List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                    soLuong = chiTietHoaDons.Count();
                }
                ViewBag.soLuong = soLuong;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Index([Bind("HoTen,Email,Sdt,DiaChi,TaiKhoan")] KhachHang khachHang)
        {
            khachHang.TaiKhoan.VaiTroId = 3;
            khachHang.TaiKhoan.NgayTao = DateTime.Now;
            _context.Add(khachHang);
            _context.SaveChanges();
            return Redirect("/Login");
        }
    }
}