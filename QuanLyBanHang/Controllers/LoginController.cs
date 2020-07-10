using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var message = HttpContext.Session.GetString("messageSession");
            if (message != null)
            {
                ViewBag.message = message;
                HttpContext.Session.Remove("messageSession");
            }
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
            var tk = _context.TaiKhoan.Include("VaiTro").Where(t => t.Username == taiKhoan.Username && t.Password == taiKhoan.Password).FirstOrDefault();
            if (tk != null)
            {
                switch (tk.VaiTro.VaiTroId)
                {
                    case 1:
                        HttpContext.Session.SetString("sessionUser", JsonConvert.SerializeObject(taiKhoan));
                        HttpContext.Session.SetInt32("VaiTroSession", tk.VaiTro.VaiTroId);
                        return Redirect("/Admin");
                    case 2:
                        HttpContext.Session.SetString("sessionUser", JsonConvert.SerializeObject(taiKhoan));
                        HttpContext.Session.SetInt32("VaiTroSession", tk.VaiTro.VaiTroId);
                        return Redirect("/SanPham");
                    case 3:
                        HttpContext.Session.SetString("sessionUser", JsonConvert.SerializeObject(taiKhoan));
                        HttpContext.Session.SetInt32("VaiTroSession", tk.VaiTro.VaiTroId);
                        return Redirect("/Homepage");
                    default:
                        return View("/Views/Login/Index.cshtml");
                }
            }
            else
            {
                HttpContext.Session.SetString("messageSession", "Sai tài khoản hoặc tên đăng nhập.");
                return View();
            }
           
        }
    }
}