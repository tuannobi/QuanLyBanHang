using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class HomepageController : Controller
    {

        private readonly QuanLyBanHangDbContext context;

        public HomepageController(QuanLyBanHangDbContext context)
        {
            this.context = context;
        }


        public IActionResult Index()
        {
            var sessionUser = HttpContext.Session.GetString("sessionUser");
            var gioHangSession = HttpContext.Session.GetString("gioHangSession");
            var trangDiemSanPhams = context.SanPham.Include("PhanLoai").Include("ChiTietKhuyenMai").Where(sp => sp.PhanLoai.NhomLoai == 2 && sp.ChiTietKhuyenMai.All(km => km.KhuyenMai.NgayBatDau <= DateTime.Now && km.KhuyenMai.NgayKetThuc >= DateTime.Now)).Union(context.SanPham.Include("PhanLoai").Include("ChiTietKhuyenMai").Where(sp => sp.PhanLoai.NhomLoai == 2 && sp.ChiTietKhuyenMai.Any(km => km.KhuyenMai.NgayBatDau >= DateTime.Now || km.KhuyenMai.NgayKetThuc <= DateTime.Now)));
            var chamSocDaSanPhams = context.SanPham.Include("PhanLoai").Include("ChiTietKhuyenMai").Where(sp => sp.PhanLoai.NhomLoai == 3 && sp.ChiTietKhuyenMai.All(km => km.KhuyenMai.NgayBatDau <= DateTime.Now && km.KhuyenMai.NgayKetThuc >= DateTime.Now)).Union(context.SanPham.Include("PhanLoai").Include("ChiTietKhuyenMai").Where(sp => sp.PhanLoai.NhomLoai == 3 && sp.ChiTietKhuyenMai.Any(km => km.KhuyenMai.NgayBatDau >= DateTime.Now || km.KhuyenMai.NgayKetThuc <= DateTime.Now))); 
            var nuocHoaSanPhams = context.SanPham.Include("PhanLoai").Include("ChiTietKhuyenMai").Where(sp => sp.PhanLoai.NhomLoai == 4 && sp.ChiTietKhuyenMai.All(km => km.KhuyenMai.NgayBatDau <= DateTime.Now && km.KhuyenMai.NgayKetThuc >= DateTime.Now)).Union(context.SanPham.Include("PhanLoai").Include("ChiTietKhuyenMai").Where(sp => sp.PhanLoai.NhomLoai == 4 && sp.ChiTietKhuyenMai.Any(km => km.KhuyenMai.NgayBatDau >= DateTime.Now || km.KhuyenMai.NgayKetThuc <= DateTime.Now)));
            /*var trangDiemSanPhams = from s in context.SanPham
                                    join p in context.PhanLoai
                                    on s.PhanLoaiId equals p.PhanLoaiId
                                    where p.NhomLoai == 1
                                    select s;
            var chamSocDaSanPhams = from s in context.SanPham
                                    join p in context.PhanLoai
                                    on s.PhanLoaiId equals p.PhanLoaiId
                                    where p.NhomLoai == 2
                                    select s;
            var nuocHoaSanPhams = from s in context.SanPham
                                  join p in context.PhanLoai
                                  on s.PhanLoaiId equals p.PhanLoaiId
                                  where p.NhomLoai == 3
                                  select s;
            var chiTietKhuyenMais = from km in context.ChiTietKhuyenMai
                                    select km;*/
            int soLuong=0;
            if (sessionUser != null)
            {
               TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
                var username = taiKhoanSession.Username;               
                ViewBag.username = username;   
            }
            if (gioHangSession != null)
            {
                List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                soLuong = chiTietHoaDons.Count();
            }
            ViewBag.soLuong = soLuong;
            //ViewBag.chiTietKhuyenMais = chiTietKhuyenMais;
            ViewBag.trangDiemSanPhams = trangDiemSanPhams;
            ViewBag.chamSocDaSanPhams = chamSocDaSanPhams;
            ViewBag.nuocHoaSanPhams = nuocHoaSanPhams;
            return View();
        }

        public IActionResult TimKiem(string keyword)
        {
            List<SanPham> sanPhams = context.SanPham.Where(sp => sp.TenSanPham.Contains(keyword)).ToList();
            HttpContext.Session.SetString("sanPhamListSession", JsonConvert.SerializeObject(sanPhams));
            //
            var ThuongHieuList = context.SanPham.Select(sp => sp.ThuongHieu).Distinct().ToList();
            Console.WriteLine(ThuongHieuList.Count());
            var productList = sanPhams;
            ViewBag.productList = productList;
            ViewBag.ThuongHieuList = ThuongHieuList;
            return View("/Views/Category/Index.cshtml");
        }

        [ServiceFilter(typeof(ClientFilter))]
        public IActionResult LichSuDonHang()
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            KhachHang khachHang = context.KhachHang.Include("TaiKhoan").Where(kh => kh.TaiKhoan.Username == taiKhoanSession.Username).FirstOrDefault();
            List<HoaDon> hoaDons = context.HoaDon.Include("PhiShip").Where(hd=>hd.TrangThai=="Đã xử lý" && hd.KhachHangId==7).ToList();
            ViewBag.KhachHang = khachHang;
            ViewBag.TTHD = hoaDons;
            return View();
        }
        [HttpPost]
        public IActionResult LichSuDonHang(KhachHang khachHang)
        {
            //TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            //KhachHang khachHangTemp = context.KhachHang.Include("TaiKhoan").Where(kh => kh.TaiKhoan.Username == taiKhoanSession.Username).FirstOrDefault();
            //khachHang.TaiKhoanId = khachHangTemp.TaiKhoan.TaiKhoanId;
            KhachHang khach = context.KhachHang.Include("TaiKhoan").Where(kh => kh.KhachHangId == khachHang.KhachHangId).FirstOrDefault();
            khach.HoTen = khachHang.HoTen;
            khach.Sdt = khachHang.Sdt;
            khach.DiaChi = khachHang.DiaChi;
            khach.NgaySinh = khachHang.NgaySinh;
            khach.TaiKhoan.Password = khachHang.TaiKhoan.Password;
            context.SaveChanges();
            return Redirect("/Homepage");
        }
    }
}