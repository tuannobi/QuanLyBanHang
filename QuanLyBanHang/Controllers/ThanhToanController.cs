using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyBanHang.Models;
using System.Text;

namespace QuanLyBanHang.Controllers
{
    public class ThanhToanController : Controller
    {
        private readonly QuanLyBanHangDbContext context;

        public ThanhToanController(QuanLyBanHangDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            KhachHang khachHang = null;
            var gioHangSession = HttpContext.Session.GetString("gioHangSession");
            var sessionUser = HttpContext.Session.GetString("sessionUser");
            int soLuong = 0;
            if (gioHangSession != null)
            {
                List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                soLuong = chiTietHoaDons.Count();
                //2 trường hợp là người dùng đã có tài khoản đặt mua và người dùng không cần đăng nhập để đặt mua
                if (sessionUser != null) //Tức là có phiên đăng nhập
                {
                    Console.OutputEncoding = Encoding.Unicode;
                    Console.InputEncoding = Encoding.Unicode;
                    Console.WriteLine("Hiện có phiên đăng nhập");
                    TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
                    khachHang = context.KhachHang.Where(kh => kh.TaiKhoan.Username == taiKhoanSession.Username).FirstOrDefault();
                    Console.WriteLine("Email cuar khach hang la: " + khachHang.Email);
                }
                var sanPhams = context.SanPham;
                var phiShips = context.PhiShip;
                float tongTienTamTinh = 0;
                foreach (ChiTietHoaDon ctdh in chiTietHoaDons)
                {
                    tongTienTamTinh += (float)ctdh.TongTien;
                }
                Console.WriteLine(sanPhams.Count());
                ViewBag.sanPhamList = sanPhams;
                ViewBag.phiShipList = phiShips;
                ViewBag.tongTienTamTinh = tongTienTamTinh;
                ViewBag.khachHang = khachHang;
                ViewBag.soLuong = soLuong;
                return View(chiTietHoaDons);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(string SoNha, int quan, float tongTienTamTinh, float tongTienThanhToan,string GhiChu,
            string HoTen, string Email, string SoDienThoai)
        {
            //2 trường hợp là người dùng đã có tài khoản đặt mua và người dùng không cần đăng nhập để đặt mua
            var sessionUser = HttpContext.Session.GetString("sessionUser");
            var gioHangSession = HttpContext.Session.GetString("gioHangSession");
            if (sessionUser != null && gioHangSession != null) //Tức là có phiên đăng nhập
            {
                List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
                KhachHang khachHang = context.KhachHang.Where(kh => kh.TaiKhoan.Username == taiKhoanSession.Username).FirstOrDefault();
                PhiShip phiShip = context.PhiShip.Where(ps => ps.PhiShipId == quan).FirstOrDefault();
                HoaDon hoaDon = new HoaDon();
                hoaDon.PhuongThucThanhToan = "SHIPCODE";
                hoaDon.SoNha = SoNha;
                hoaDon.Quan = phiShip.Quan;
                hoaDon.PhiShipId = quan;
                hoaDon.KhachHangId = khachHang.KhachHangId;
                hoaDon.TongTien = tongTienTamTinh;
                hoaDon.TongTienThanhToan = tongTienThanhToan;
                hoaDon.GhiChu = GhiChu;
                hoaDon.TrangThai = "Chờ xử lý";
                hoaDon.ThoiGianChoXuLy = DateTime.Now;
                hoaDon.ChiTietHoaDon = chiTietHoaDons;
                context.Add(hoaDon);
                context.SaveChanges();
            }else if( sessionUser==null && gioHangSession!=null)//Không có phiên đăng nhập
            {
                List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                PhiShip phiShip = context.PhiShip.Where(ps => ps.PhiShipId == quan).FirstOrDefault();
                KhachHang khachHang = new KhachHang();
                khachHang.HoTen = HoTen;
                khachHang.Email = Email;
                khachHang.Sdt = SoDienThoai;
                khachHang.DiaChi = SoNha;

                HoaDon hoaDon = new HoaDon();
                hoaDon.PhuongThucThanhToan = "SHIPCODE";
                hoaDon.SoNha = SoNha;
                hoaDon.Quan = phiShip.Quan;
                hoaDon.PhiShipId = quan;
                hoaDon.KhachHang = khachHang ;
                hoaDon.TongTien = tongTienTamTinh;
                hoaDon.TongTienThanhToan = tongTienThanhToan;
                hoaDon.GhiChu = GhiChu;
                hoaDon.TrangThai = "Chờ xử lý";
                hoaDon.ThoiGianChoXuLy = DateTime.Now;
                hoaDon.ChiTietHoaDon = chiTietHoaDons;
                context.Add(hoaDon);
                context.SaveChanges();
            }
            return Redirect("/Homepage");
        }
    }
}