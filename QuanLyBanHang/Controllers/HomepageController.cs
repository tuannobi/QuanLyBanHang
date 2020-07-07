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
    [ServiceFilter(typeof(ClientFilter))]
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
                                    select km;
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
    }
}