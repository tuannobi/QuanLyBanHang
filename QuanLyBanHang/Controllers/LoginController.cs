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
    public class LoginController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public LoginController(QuanLyBanHangDbContext context)
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
        public IActionResult Index([Bind("Username,Password,VaiTroId")] TaiKhoan taiKhoan)
        {
            var tk = _context.TaiKhoan.Where(t => t.Username == taiKhoan.Username && t.Password == taiKhoan.Password && t.VaiTroId==3).FirstOrDefault();
            Console.WriteLine("Ma tai khoan vua dang nhap la: " +tk.TaiKhoanId);
            if (tk != null)
            {
                HttpContext.Session.SetString("sessionUser", JsonConvert.SerializeObject(taiKhoan));
                return Redirect("/Homepage");
            }
            else
            {
                return View("/Views/Login/Index.cshtml");
            }
        }
    }
}